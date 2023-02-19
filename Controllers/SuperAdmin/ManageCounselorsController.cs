using icounselvault.Models.Auth;
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

        public IActionResult ViewCounselors()
        {
            var counselors = _context.COUNSELOR
                .Include(c => c.user)
                .ToList();
            return View("../../Views/SuperAdmin/ManageCounselors/ViewCounselors", counselors);
        }

        public IActionResult ShowAddEditCounselorUser(int userId, string encryptedPassword)
        {
            if (encryptedPassword != "-1")
            {
                EncryptDecryptText encryptDecryptText = new();
                TempData["selectedUser"] = _context.USER
                    .Where(u => u.USER_ID == userId)
                    .FirstOrDefault();
                TempData["decryptedPassword"] = encryptDecryptText.DecryptText(encryptedPassword);
            }
            return View("../../Views/SuperAdmin/ManageCounselors/AddEditCounselor", GetCounselorUsers());
        }

        public IActionResult ShowEditCounselor(int counselorId)
        {
            TempData["selectedCounselor"] = _context.COUNSELOR
                .Where(co => co.COUNSELOR_ID == counselorId)
                .FirstOrDefault();
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
    }
}
