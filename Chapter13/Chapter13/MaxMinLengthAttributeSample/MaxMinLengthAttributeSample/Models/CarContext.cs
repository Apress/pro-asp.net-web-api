using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MaxMinLengthAttributeSample.Models {

    public class CarsContext {

        private static int _nextId = 9;

        //cars store
        private readonly static ConcurrentDictionary<int, Car> _carsDictionary = new ConcurrentDictionary<int, Car>(new HashSet<KeyValuePair<int, Car>> { 
            new KeyValuePair<int, Car>(1, new Car { Id = 1, Make = "Make1", Model = "Model1", Year = 2010, Price = 10732.2F, Tags = new[] { "Familly", "Minivan" }  }),
            new KeyValuePair<int, Car>(2, new Car { Id = 2, Make = "Make2", Model = "Model2", Year = 2008, Price = 27233.1F, Tags = new[] { "Minivan" } }),
            new KeyValuePair<int, Car>(3, new Car { Id = 3, Make = "Make3", Model = "Model1", Year = 2009, Price = 67437.0F, Tags = new[] { "Sport", "High Speed" } }),
            new KeyValuePair<int, Car>(4, new Car { Id = 4, Make = "Make4", Model = "Model3", Year = 2007, Price = 78984.2F, Tags = new[] { "SUV", "Fuel Efficiency", } })
        });

        public IEnumerable<Car> All {
            get {
                return _carsDictionary.Values;
            }
        }

        public IEnumerable<Car> Get(Func<Car, bool> predicate) {

            return _carsDictionary.Values.Where(predicate);
        }

        public Tuple<bool, Car> GetSingle(int id) {

            Car car;
            var doesExist = _carsDictionary.TryGetValue(id, out car);
            return new Tuple<bool, Car>(doesExist, car);
        }

        public Car GetSingle(Func<Car, bool> predicate) {

            return _carsDictionary.Values.FirstOrDefault(predicate);
        }

        public Car Add(Car car) {

            car.Id = _nextId;
            _carsDictionary.TryAdd(car.Id, car);
            _nextId++;

            return car;
        }

        public bool TryRemove(int id) {

            Car removedCar;
            return _carsDictionary.TryRemove(id, out removedCar);
        }

        public bool TryUpdate(Car car) {

            Car oldCar;
            if (_carsDictionary.TryGetValue(car.Id, out oldCar)) {

                return _carsDictionary.TryUpdate(car.Id, car, oldCar);
            }

            return false;
        }
    }
}