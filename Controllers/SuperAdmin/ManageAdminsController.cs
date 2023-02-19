using icounselvault.Models.Auth;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;

namespace icounselvault.Controllers.SuperAdmin
{
    public class ManageAdminsController : Controller
    {
        private readonly AppDbContext _context;
        public ManageAdminsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ViewAdmins()
        {
            var admins = _context.USER
                .Where(u => u.PRIVILEGE_TYPE == "ADMIN" || u.PRIVILEGE_TYPE == "SUPER_ADMIN")
                .ToList();
            return View("../../Views/SuperAdmin/ManageAdmins/ViewAdmins", admins);
        }

        public IActionResult ShowAddEditAdmin(int userId, string encryptedPassword)
        {
            if (encryptedPassword != "-1")
            {
                EncryptDecryptText encryptDecryptText = new();
                TempData["selectedAdmin"] = _context.USER
                    .Where(u => u.USER_ID == userId)
                    .FirstOrDefault();
                TempData["decryptedPassword"] = encryptDecryptText.DecryptText(encryptedPassword);
            }
            return View("../../Views/SuperAdmin/ManageAdmins/AddEditAdmin");
        }

        public IActionResult AddAdmin(string username, string password, string confirmPassword, string type)
        {
            EncryptDecryptText encryptDecryptText = new();
            if (username != null && password != null && confirmPassword == password)
            {
                User newAdmin = new()
                {
                    USERNAME = username,
                    PASSWORD = encryptDecryptText.EncryptText(password),
                    PRIVILEGE_TYPE = type,
                    USER_STATUS = "ACT"
                };
                _context.USER.Add(newAdmin);
                _context.SaveChanges();
                var admins = _context.USER
                    .Where(u => u.PRIVILEGE_TYPE == "ADMIN" || u.PRIVILEGE_TYPE == "SUPER_ADMIN")
                    .ToList();
                return View("../../Views/SuperAdmin/ManageAdmins/ViewAdmins", admins);
            }
            else
            {
                ModelState.AddModelError("", "Error, Please enter valid values into the fields.");
                return View("../../Views/SuperAdmin/ManageAdmins/AddEditAdmin");
            }
        }

        [HttpPost]
        public IActionResult EditAdmin(string userId, string username, string password, string confirmPassword, string type, string status) 
        {
            EncryptDecryptText encryptDecryptText = new();
            var foundAdmin = _context.USER
                        .Where(u => u.USER_ID == int.Parse(userId))
                        .FirstOrDefault();
            if (username != null && password != null && confirmPassword == password)
            {

                foundAdmin.USERNAME = username;
                foundAdmin.PASSWORD = encryptDecryptText.EncryptText(password);
                foundAdmin.PRIVILEGE_TYPE = type;
                foundAdmin.USER_STATUS = status;
                _context.SaveChanges();
                var admins = _context.USER
                    .Where(u => u.PRIVILEGE_TYPE == "ADMIN" || u.PRIVILEGE_TYPE == "SUPER_ADMIN")
                    .ToList();
                return View("../../Views/SuperAdmin/ManageAdmins/ViewAdmins", admins);
            }
            else
            {
                ModelState.AddModelError("", "Error, Please enter valid values into the fields.");
                TempData["selectedAdmin"] = foundAdmin;
                TempData["decryptedPassword"] = encryptDecryptText.DecryptText(foundAdmin.PASSWORD);
                return View("../../Views/SuperAdmin/ManageAdmins/AddEditAdmin");
            }
        }
    }
}
