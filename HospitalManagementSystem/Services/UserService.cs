using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem.BL;
using HospitalManagementSystem.DL;

namespace HospitalManagementSystem.Services
{
    public class UserService
    {
        private readonly UserRepo _userRepo;

        public UserService()
        {
            _userRepo = new UserRepo();
        }

        // Sign Up - Create new user with validation
        public bool SignUp(string email, string password, string role)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return false;

            User user = new User(email, password, role);

            // Business validation
            if (!user.ValidateEmail())
                return false;

            if (!user.ValidatePassword())
                return false;

            if (!user.ValidateRole())
                return false;

            // Attempt to add to database
            return _userRepo.AddUser(user);
        }

        // Sign Up and return the created User object (or null on failure)
        public User SignUpReturnUser(string email, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            User user = new User(email, password, role);

            if (!user.ValidateEmail() || !user.ValidatePassword() || !user.ValidateRole())
                return null;

            bool added = _userRepo.AddUser(user);
            return added ? user : null;
        }

        // Sign In - Authenticate user
        public User SignIn(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            return _userRepo.SignIn(email, password);
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _userRepo.GetAll();
        }

        // Get user by ID
        public User GetUserById(int userId)
        {
            return _userRepo.GetById(userId);
        }

        // Get users by role
        public List<User> GetUsersByRole(string role)
        {
            return _userRepo.GetByRole(role);
        }

        // Update user
        public bool UpdateUser(User user)
        {
            if (user == null)
                return false;

            // Validate before updating
            if (!user.ValidateEmail() || !user.ValidatePassword() || !user.ValidateRole())
                return false;

            return _userRepo.Update(user);
        }

        // Delete user
        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
                return false;

            return _userRepo.Delete(userId);
        }

        // Check if user is admin
        public bool IsAdmin(User user)
        {
            return user != null && user.IsAdmin();
        }

        // Check if user is doctor
        public bool IsDoctor(User user)
        {
            return user != null && user.IsDoctor();
        }

        // Check if user is patient
        public bool IsPatient(User user)
        {
            return user != null && user.IsPatient();
        }
    }
}
