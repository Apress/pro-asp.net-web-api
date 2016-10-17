(function(window, ko, _, $) {

    if (!ko) {
        throw "Knockoutjs library was not found.";
    }

    if (!_) {
        throw "underscorejs library was not found.";
    }

    if (!$) {
        throw "jquery library was not found.";
    }


    var     NEW_ORDER = "New order",
           EDIT_ORDER = "Editing #",
           PLACE_ORDER = "Place order",
           UPDATE_ORDER = "Update order",
           rootUrl = window.location.href,
           rootNamespace;

    if (rootUrl.indexOf("/#"))
        rootUrl = rootUrl.replace("/#", "");
    rootUrl += "/api/Order";

    function namespace(namespaceString) {
        var parts = namespaceString.split('.'),
            parent = window,
            currentPart = '';

        for (var i = 0, length = parts.length; i < length; i++) {
            currentPart = parts[i];
            parent[currentPart] = parent[currentPart] || {};
            parent = parent[currentPart];
        }

        return parent;
    }

    namespace("ProAspNetWebApi.Samples.UnitTesting.Pizza");

    rootNamespace = ProAspNetWebApi.Samples.UnitTesting.Pizza;

    rootNamespace.MainViewModel = function MainViewModel() {

        var that = this;
        this.rootUrl = rootUrl;
        this.orders = ko.observableArray();
        this.customerName = ko.observable();
        this.orderTitle = ko.observable(NEW_ORDER);
        this.buttonText = ko.observable(PLACE_ORDER);
        this.isUpdateOrderMode = ko.computed(function() {
            return that.buttonText() !== PLACE_ORDER;
        });

        this.PizzaMenu = [
            "Hawaiian",
            "Meat Feast",
            "Spicy Bacon",
            "Inferno",
            "Vegetarian",
            "Four Season",
            "Ham and Mushroom",
            "Pepperoni"
        ];
        
        this.currentOrderItems = ko.observableArray(_.map(this.PizzaMenu, function(x) {
            return new rootNamespace.OrderItem(x);
        }));
        
        this.canAdd = ko.computed(function () {
            var anyPizzaChosen = _.reduce(that.currentOrderItems(), function(memo, x) { return memo + x.quantity(); }, 0) > 0,
                customerHasName = that.customerName() != undefined && that.customerName()!="";
            return anyPizzaChosen && customerHasName;
             
        });

        this.deleteOrder = function () {
            var localThat = this;
            $.ajax({
                type: "DELETE",
                url: this.location
            })
                .success(function (data, text, xhr) {
                    that.orders.remove(localThat);
                });
        };

        this.loadOrderForUpdate = function() {
            that._loadOrder(this);
            that.currentOrder = this;
        };
        
        // load existing orders
        $.ajax({
            type: "GET",
            url: that.rootUrl
            })
            .success(function (data, text, xhr) {
                var ord;
                _.forEach(data, function (x) {
                    ord = new rootNamespace.Order(x);
                    ord.location = that.rootUrl + "/" + ord.id();
                    that.orders.push(ord);
                });
            });
    };

    rootNamespace.MainViewModel.prototype._loadOrder = function (order) {
        var that = this,
            found;
        if (order) {
            
            _.forEach(that.currentOrderItems(), function (x) {

                found = _.find(order.items(), function(itm) {
                    return itm.name == x.name();
                });
                
                if (found) {
                    x.quantity(found.quantity);
                } else {
                    x.quantity(0);
                }

            });

            this.orderTitle(EDIT_ORDER + order.id());
            this.buttonText(UPDATE_ORDER);
            this.customerName(order.customerName());

        } else {
            
            // reset quantity of pizzas
            _.forEach(that.currentOrderItems(), function(x) {
                x.quantity(0);
            });
            
            // reset text and labels
            this.orderTitle(NEW_ORDER);
            this.buttonText(PLACE_ORDER);
            this.customerName(undefined);
        }
    };

    rootNamespace.MainViewModel.prototype.cancelUpdate = function () {
        this._loadOrder(null);
    };

    rootNamespace.MainViewModel.prototype.up = function() {
        this.quantity(this.quantity()+1);
    };

    rootNamespace.MainViewModel.prototype.down = function () {
        this.quantity(Math.max(this.quantity()-1,0));
    };

    

    rootNamespace.MainViewModel.prototype.placeOrder = function () {
        var that = this,
            order = null;
        if (this.buttonText() == PLACE_ORDER) { 
            // New order
            $.ajax({
                type: "POST",
                data: this._buildOrders(),
                url: this.rootUrl
                })
                .success(function(data, text, xhr) {
                    that.addOrder(xhr.getResponseHeader("Location"));
                });
                
        } else { // update order
            var ord = this._buildOrders();
            ord.id = that.currentOrder.id();
            $.ajax({
                
                type: "PUT",
                data: ord,
                url: this.rootUrl
            })
                .success(function (data, text, xhr) {
                    that.addOrder(that.currentOrder.location);
                    that.cancelUpdate();
                });
        }
            
    };
    
   

    rootNamespace.MainViewModel.prototype.addOrder = function addOrder(location) {
        var that = this,
            tmp = null;
        $.ajax({ url: location })
            .success(function (data, text, xhr) {
                var newOrder = new rootNamespace.Order(data);
                newOrder.location = location;
                tmp = _.find(that.orders(), function (x) { return x.id() == data.id; });
                if (tmp) { // update 
                    // copy
                    tmp.id(newOrder.id());
                    tmp.customerName(newOrder.customerName());
                    tmp.totalPrice(newOrder.totalPrice());
                    tmp.items(newOrder.items());

                } else { // place order
                    that.orders.push(newOrder);
                }
            });
    };

    rootNamespace.MainViewModel.prototype._buildOrders = function () {
        return new rootNamespace.OrderDto(this.customerName(),
            _.reduce(this.currentOrderItems(), 
                function(memo, x) {
                    if (x.quantity())
                        memo.push(new rootNamespace.OrderItemDto(x.name(), x.quantity()));
                    return memo;
                }, [] )
            );
    };
    
    rootNamespace.Order = function Order(data) {
        var that = this;
        this.items =ko.observableArray(data.items);
        this.id = ko.observable(data.id);
        this.customerName = ko.observable(data.customerName);
        this.totalPrice = ko.observable(data.totalPrice);
        this.totalPriceFormatted = ko.computed(function() {
            return that.totalPrice().toFixed(2) + " $";
        });
    };
    
    rootNamespace.OrderDto = function OrderDto(customerName, items) {
        this.items = items;
        this.id = 0;
        this.customerName = customerName;
        this.totalPrice = 0;
    };

    rootNamespace.OrderItem = function OrderItem(name) {
        this.name = ko.observable(name);
        this.quantity = ko.observable(0);
    };
    
    rootNamespace.OrderItemDto = function OrderItemDto(name, quantity) {
        this.name = name;
        this.quantity = quantity;
    };

})(this, ko, _, $);