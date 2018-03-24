using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void find_products_that_price_between_200_and_500_cost_higher_then30()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.JasonWhere((product, index) =>
                product.Price > 200 && product.Price < 500 && product.Cost > 30 && index % 3 == 0);

            var expected = new List<Product>()
            {
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            foreach (var product in actual)
            {
                Console.WriteLine(((Product)product).Price);
            }

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_Employee_that_age_greater_then_30()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.JasonWhere(employee => employee.Age > 30);

            var expected = new List<Employee>()
            {
                new Employee {Name = "Joe", Role = RoleType.Engineer, MonthSalary = 100, Age = 44, WorkingYear = 2.6},
                new Employee {Name = "Tom", Role = RoleType.Engineer, MonthSalary = 140, Age = 33, WorkingYear = 2.6},
                new Employee {Name = "Kevin", Role = RoleType.Manager, MonthSalary = 380, Age = 55, WorkingYear = 2.6},
                new Employee {Name = "Bas", Role = RoleType.Engineer, MonthSalary = 280, Age = 36, WorkingYear = 2.6},
                new Employee {Name = "Joey", Role = RoleType.Engineer, MonthSalary = 250, Age = 40, WorkingYear = 2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void find_products_that_price_between_200_and_500_2()
        {
            var products = RepositoryFactory.GetProducts();
            var actual = products.JasonWhere(x => x.Price < 500 && x.Price > 200 && x.Cost > 30);

            var expected = new List<Product>()
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Select_URL_Should_return_All_Https()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.JasonSelect(x => x.Replace("http:", "https:"));
            ;

            var expected = new List<string>()
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void ReturnUrlLengh()
        {
            var urls = RepositoryFactory.GetUrls();
            var actual = urls.JasonSelect(x => x.Length);

            var expected = new List<int>()
            {
                19,
                20,
                19,
                17
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Return_Employee_Role_And_Name()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = employees.JasonWhere(x => x.Age < 25).JasonSelect(x => x.Role.ToString() + ":" + x.Name);

            var expected = new List<string>()
            {
                "OP:Andy",
                "Engineer:Frank",
            };

            foreach (var title in actual)
            {
                Console.Write(title);
            }

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Take2()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.JasonTake(employees, 2);

            var expected = new List<Employee>()
            {
                new Employee{Name="Joe", Role=RoleType.Engineer, MonthSalary=100, Age=44, WorkingYear=2.6 } ,
                new Employee{Name="Tom", Role=RoleType.Engineer, MonthSalary=140, Age=33, WorkingYear=2.6} ,
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Skip6()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.JasonSkip(employees, 6);

            var expected = new List<Employee>()
            {
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void Group()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.JasonGroupBy(employees, 3, employee => employee.MonthSalary);

            var expected = new List<int>()
            {
                620,
                540,
                370
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void TakeWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.TakeWhile(employees, 2, employee => employee.MonthSalary > 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6}
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [TestMethod]
        public void SkipWhile()
        {
            var employees = RepositoryFactory.GetEmployees();
            var actual = WithoutLinq.SkipWhile(employees, 3, employee => employee.MonthSalary < 150);

            var expected = new List<Employee>()
            {
                new Employee{Name="Kevin", Role=RoleType.Manager, MonthSalary=380, Age=55, WorkingYear=2.6} ,
                new Employee{Name="Bas", Role=RoleType.Engineer, MonthSalary=280, Age=36, WorkingYear=2.6} ,
                new Employee{Name="Mary", Role=RoleType.OP, MonthSalary=180, Age=26, WorkingYear=2.6} ,
                new Employee{Name="Frank", Role=RoleType.Engineer, MonthSalary=120, Age=16, WorkingYear=2.6} ,
                new Employee{Name="Joey", Role=RoleType.Engineer, MonthSalary=250, Age=40, WorkingYear=2.6},
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }
    }

    internal static class WithoutLinq
    {
        public static IEnumerable<T> Find<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> FindProducts<T>(this IEnumerable<T> items, Func<T, int, bool> predicate)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        internal static IEnumerable<string> Select(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                yield return item.Replace("http:", "https:");
            }
        }

        public static IEnumerable<int> GetUrlLength(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                yield return item.Length;
            }
        }

        internal static IEnumerable<T> JasonTake<T>(IEnumerable<T> items, int count)
        {
            var enumerator = items.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index < count)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }

                index++;
            }
        }

        public static IEnumerable<T> JasonSkip<T>(IEnumerable<T> items, int count)
        {
            var enumerator = items.GetEnumerator();
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (index >= count)
                {
                    yield return enumerator.Current;
                }

                index++;
            }
        }

        public static IEnumerable<int> JasonGroupBy<T>(IEnumerable<T> items, int pageSize, Func<T, int> property)
        {
            var rowIndex = 0;
            while (rowIndex < items.Count())
            {
                yield return items.Skip(rowIndex).Take(pageSize).Sum(property);
                rowIndex += pageSize;
            }
        }

        public static IEnumerable<T> TakeWhile<T>(IEnumerable<T> items, int count, Func<T, bool> predicate)
        {
            var ItemIndex = 0;
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    ItemIndex++;
                    yield return item;
                    if (ItemIndex >= count)
                    {
                        yield break;
                    }
                }
            }
        }

        public static IEnumerable<T> SkipWhile<T>(IEnumerable<T> items, int count, Func<T, bool> predicate)
        {
            var item = items.GetEnumerator();
            var index = 0;
            while (item.MoveNext())
            {
                if (predicate(item.Current) && index < count)
                {
                    index++;
                }
                else
                {
                    yield return item.Current;
                }
            }
        }
    }

    internal static class YourOwnLinq
    {
        public static IEnumerable<TItem> JasonWhere<TItem>(this IEnumerable<TItem> items, Func<TItem, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TItem> JasonWhere<TItem>(this IEnumerable<TItem> items,
            Func<TItem, int, bool> predicate)
        {
            int index = 0;
            foreach (var item in items)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        public static IEnumerable<TResult> JasonSelect<TSource, TResult>(this IEnumerable<TSource> items,
            Func<TSource, TResult> selector)
        {
            foreach (var item in items)
            {
                yield return selector(item);
            }
        }
    }
}