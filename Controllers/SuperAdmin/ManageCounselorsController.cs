using icounselvault.Models.Auth;
using icounselvault.Models.Profiles;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace icounselvault.Controllers.SuperAdmin
{
    public class ManageCounselorsController : Controller
    {
        private readonly AppDbContext _context;
        public ManageCounselorsController(AppDbContext context)
        {
            _context = context;
        }

        [Authorization(RequiredPrivilegeType = "SUPER_ADMIN")]
        public IActionResult SuperAdminViewCounselors()
        {
            TempData["layout"] = "../../Views/Shared/_SuperAdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/ViewCounselors", GetCounselorList());
        }

        [Authorization(RequiredPrivilegeType = "ADMIN")]
        public IActionResult AdminViewCounselors()
        {
            TempData["layout"] = "../../Views/Shared/_AdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/ViewCounselors", GetCounselorList());
        }

        [Authorization(RequiredPrivilegeType = "SUPER_ADMIN")]
        public IActionResult SuperAdminShowAddEditCounselorUser(int userId, string encryptedPassword)
        {
            SetTempDataForManageCounselorUsers(userId, encryptedPassword);
            TempData["layout"] = "../../Views/Shared/_SuperAdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
        }

        [Authorization(RequiredPrivilegeType = "ADMIN")]
        public IActionResult AdminShowAddEditCounselorUser(int userId, string encryptedPassword)
        {
            SetTempDataForManageCounselorUsers(userId, encryptedPassword);
            TempData["layout"] = "../../Views/Shared/_AdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
        }

        [Authorization(RequiredPrivilegeType = "SUPER_ADMIN")]
        public IActionResult SuperAdminShowEditCounselor(int counselorId)
        {
            SetTempDataForSelectedCounselor(counselorId);
            TempData["layout"] = "../../Views/Shared/_SuperAdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor");
        }

        [Authorization(RequiredPrivilegeType = "ADMIN")]
        public IActionResult AdminShowEditCounselor(int counselorId)
        {
            SetTempDataForSelectedCounselor(counselorId);
            TempData["layout"] = "../../Views/Shared/_AdminLayout";
            return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor");
        }

        [HttpPost]
        public IActionResult AddCounselorUser(string username, string password, string confirmPassword)
        {
            EncryptDecryptText encryptDecryptText = new();
            if (username != null && password != null && confirmPassword == password)
            {
                User newAdmin = new()
                {
                    USERNAME = username,
                    PASSWORD = encryptDecryptText.EncryptText(password),
                    PRIVILEGE_TYPE = "COUNSELOR",
                    USER_STATUS = "ACT"
                };
                _context.USER.Add(newAdmin);
                _context.SaveChanges();
                return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
            }
            else
            {
                ModelState.AddModelError("", "Error, Please enter valid values into the fields.");
                return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
            }
        }

        [HttpPost]
        public IActionResult EditCounselorUser(string userId, string username, string password, string confirmPassword, string status)
        {
            EncryptDecryptText encryptDecryptText = new();
            var foundCounselorUser = _context.USER
                        .Where(u => u.USER_ID == int.Parse(userId))
                        .FirstOrDefault();
            if (username != null && password != null && confirmPassword == password)
            {

                foundCounselorUser.USERNAME = username;
                foundCounselorUser.PASSWORD = encryptDecryptText.EncryptText(password);
                foundCounselorUser.USER_STATUS = status;
                _context.SaveChanges();
                return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
            }
            else
            {
                ModelState.AddModelError("", "Error, Please enter valid values into the fields.");
                TempData["selectedUser"] = foundCounselorUser;
                TempData["decryptedPassword"] = encryptDecryptText.DecryptText(foundCounselorUser.PASSWORD);
                return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
            }
        }

        [HttpPost]
        public IActionResult EditCounselorStatus(string counselorId, string status)
        {
            var foundCounselor = _context.COUNSELOR
                .Where(co => co.COUNSELOR_ID == int.Parse(counselorId))
                .FirstOrDefault();
            foundCounselor.COUNSELOR_STATUS = status;
            _context.SaveChanges();
            var counselors = _context.COUNSELOR
                .Include(c => c.user)
                .ToList();
            return View("../../Views/SuperAdmin/ManageCounselors/ViewCounselors", counselors);
        }

        public List<User> GetCounselorUsers()
        {
            return _context.USER
                   .Where(u => u.PRIVILEGE_TYPE == "COUNSELOR")
                   .ToList();
        }

        private List<Models.Profiles.Counselor> GetCounselorList() 
        {
            return _context.COUNSELOR
                .Include(c => c.user)
                .ToList();
        }

        private void SetTempDataForSelectedCounselor(int counselorId) 
        {
            TempData["selectedCounselor"] = _context.COUNSELOR
                .Where(co => co.COUNSELOR_ID == counselorId)
                .FirstOrDefault();
        }

        private void SetTempDataForManageCounselorUsers(int userId, string encryptedPassword) 
        {
            if (encryptedPassword != "-1")
            {
                EncryptDecryptText encryptDecryptText = new();
                TempData["selectedUser"] = _context.USER
                    .Where(u => u.USER_ID == userId)
                    .FirstOrDefault();
                TempData["decryptedPassword"] = encryptDecryptText.DecryptText(encryptedPassword);
            }
        }
    }
}
