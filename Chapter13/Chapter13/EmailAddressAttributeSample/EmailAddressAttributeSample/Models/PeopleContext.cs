using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmailAddressAttributeSample.Models {

    public class PeopleContext {

        private static int _nextId = 9;

        //people store
        private readonly static ConcurrentDictionary<int, Person> _peopleDictionary = new ConcurrentDictionary<int, Person>(new HashSet<KeyValuePair<int, Person>> { 
            new KeyValuePair<int, Person>(1, new Person { Id = 1, Name = "PersonA", Surname = "PersonSurnameA", EmailAddress = "persona@example.com" }),
            new KeyValuePair<int, Person>(2, new Person { Id = 2, Name = "PersonB", Surname = "PersonSurnameB", EmailAddress = "personb@example.com" }),
            new KeyValuePair<int, Person>(3, new Person { Id = 3, Name = "PersonC", Surname = "PersonSurnameC", EmailAddress = "personc@example.com" })
        });

        public IEnumerable<Person> All {
            get {
                return _peopleDictionary.Values;
            }
        }

        public IEnumerable<Person> Get(Func<Person, bool> predicate) {

            return _peopleDictionary.Values.Where(predicate);
        }

        public Tuple<bool, Person> GetSingle(int id) {

            Person person;
            var doesExist = _peopleDictionary.TryGetValue(id, out person);
            return new Tuple<bool, Person>(doesExist, person);
        }

        public Person GetSingle(Func<Person, bool> predicate) {

            return _peopleDictionary.Values.FirstOrDefault(predicate);
        }

        public Person Add(Person person) {

            person.Id = _nextId;
            _peopleDictionary.TryAdd(person.Id, person);
            _nextId++;

            return person;
        }

        public bool TryRemove(int id) {

            Person removedPerson;
            return _peopleDictionary.TryRemove(id, out removedPerson);
        }

        public bool TryUpdate(Person person) {

            Person oldPerson;
            if (_peopleDictionary.TryGetValue(person.Id, out oldPerson)) {

                return _peopleDictionary.TryUpdate(person.Id, person, oldPerson);
            }

            return false;
        }
    }
}