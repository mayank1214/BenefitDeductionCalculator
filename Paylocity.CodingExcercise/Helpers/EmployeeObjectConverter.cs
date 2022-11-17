using Paylocity.CodingExcercise.Core.Enums;
using Paylocity.CodingExcercise.Data.Entities;
using Paylocity.CodingExcercise.Data.Enum;
using Paylocity.CodingExcercise.ViewModels;

namespace Paylocity.CodingExcercise.Web.Helpers
{
    public static class EmployeeObjectConverter
    {
        public static List<Person> ConvertEmployeeToPersonList(EmployeeViewModel employeeViewModel)
        {
            var returnList = new List<Person>();

            returnList.Add(new Person() { Name = employeeViewModel.Name, Type = PersonType.Employee });

            foreach (var dependent in employeeViewModel.Dependents)
            {
                returnList.Add(new Person() { Name = dependent.Name, Type = ConvertDependentTypeToPersonType(dependent.DependentType) });
            }

            return returnList;
        }

        public static PersonType ConvertDependentTypeToPersonType(DependentType type)
        {
            switch (type)
            {
                case DependentType.Child:
                    return PersonType.Child;
                case DependentType.Spouse:
                    return PersonType.Spouse;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
