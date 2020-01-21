namespace ReflectionDynamicSort
{
    using Helpers;
    using Enumerations;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            var listEmployees = GetListEmployees();
            var orderList = new List<OrderModel>()
            {
                new OrderModel("Age"),
                new OrderModel("Department", OrderTypeEnum.Descending),
                new OrderModel("Name", OrderTypeEnum.Descending)
            };
            var resultListOrder = listEmployees.AsQueryable().Order(orderList);
        }


        static List<EmployeeModel> GetListEmployees()
        {
            return new List<EmployeeModel>
            {
                new EmployeeModel(1, "David", "Software Developer", 50),
                new EmployeeModel(2, "Charlotte", "Finance", 30),
                new EmployeeModel(3, "Elizabeth", "Marketing", 40),
                new EmployeeModel(4, "Thomas", "Office Administration", 35)
            };
        }
    }
}
