using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using CsvFormatterSample.Entities;

namespace CsvFormatterSample.Formatters {
    public class CarCsvMediaTypeFormatter : BufferedMediaTypeFormatter {
        private static readonly char[] SpecialChars = new char[] {',', '\n', '\r', '"'};

        public CarCsvMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        public override bool CanReadType(Type type) {
            return false;
        }

        public override bool CanWriteType(Type type) {
            if (type == typeof (Car)) {
                return true;
            }
            else {
                var enumerableType = typeof (IEnumerable<Car>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public override void WriteToStream(Type type, object value, Stream stream, HttpContent content) {
            using (var writer = new StreamWriter(stream)) {
                var cars = value as IEnumerable<Car>;
                if (cars != null) {
                    foreach (var car in cars) {
                        writeItem(car, writer);
                    }
                }
                else {
                    var car = value as Car;
                    if (car == null) {
                        throw new InvalidOperationException("Cannot serialize type");
                    }
                    writeItem(car, writer);
                }
            }
            stream.Close();
        }

        private void writeItem(Car car, StreamWriter writer) {
            writer.WriteLine("{0},{1},{2},{3},{4}", Escape(car.Id),
                             Escape(car.Make), Escape(car.Model), Escape(car.Year), Escape(car.Price));
        }

        private string Escape(object o) {
            if (o == null) {
                return "";
            }
            var field = o.ToString();
            return field.IndexOfAny(SpecialChars) != -1 ? String.Format("\"{0}\"", field.Replace("\"", "\"\"")) : field;
        }
    }
}