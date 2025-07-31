namespace FruitsAndVegetablesShopping.DAL.Repo.Abstraction
{
    public interface IAdminRepo
    {
        (List<string>?, string?) GetAllAdmins();
        (string?, string?) GetAdminById(int id);
        (bool, string?) CreateAdmin(string adminName, string createdBy);
        (bool, string?) UpdateAdmin(int id, string adminName, string modifiedBy);
        (bool, string?) DeleteAdmin(int id, string deletedBy);
    }
}
