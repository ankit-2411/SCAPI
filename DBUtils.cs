using SCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCAPI
{
    public static class DBUtils
    {
        public static int AddUser(UserBase user)
        {
            int UserId = 0;

            try
            {
                using (SC_DBDataContext dc = new SC_DBDataContext())
                {
                    var newUser = new User()
                    {
                        IncorporationDate = user.IncorporationDate,
                        Industry = user.Industry,
                        LoanPurpose = user.LoanPurpose,
                        RequestedAmount = user.RequestedAmount,
                        Revenue = user.Revenue
                    };

                    dc.Users.InsertOnSubmit(newUser);
                    dc.SubmitChanges();

                    UserId = newUser.UserId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return UserId;
        }

        public static User GetSingleUser(int userId)
        {
            User user = null;

            try
            {
                using (SC_DBDataContext dc = new SC_DBDataContext())
                {
                    var usr = dc.Users.SingleOrDefault(p => p.UserId == userId);

                    if (usr != null)
                    {
                        user = new User()
                        {
                            UserId = usr.UserId,
                            IncorporationDate = usr.IncorporationDate,
                            Industry = usr.Industry,
                            LoanPurpose = usr.LoanPurpose,
                            RequestedAmount = usr.RequestedAmount,
                            Revenue = usr.Revenue
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return user;
        }

        public static IEnumerable<User> GetUsersList()
        {
            List<User> lstUsers = new List<User>();

            try
            {
                using (SC_DBDataContext dc = new SC_DBDataContext())
                {
                    var users = dc.Users;

                    if (users != null && users.Any())
                    {
                        foreach (var usr in users)
                        {
                            if (usr != null)
                            {
                                lstUsers.Add(new User()
                                {
                                    UserId = usr.UserId,
                                    IncorporationDate = usr.IncorporationDate,
                                    Industry = usr.Industry,
                                    LoanPurpose = usr.LoanPurpose,
                                    RequestedAmount = usr.RequestedAmount,
                                    Revenue = usr.Revenue
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return lstUsers;
        }

        public static ResultStatus UpdateUser(User user)
        {
            ResultStatus result = ResultStatus.Null;

            try
            {
                using (SC_DBDataContext dc = new SC_DBDataContext())
                {
                    var dbUser = dc.Users.SingleOrDefault(u => u.UserId == user.UserId);

                    if (dbUser != null)
                    {
                        dbUser.IncorporationDate = user.IncorporationDate;
                        dbUser.Industry = user.Industry;
                        dbUser.LoanPurpose = user.LoanPurpose;
                        dbUser.RequestedAmount = user.RequestedAmount;
                        dbUser.Revenue = user.Revenue;

                        dc.SubmitChanges();
                        result = ResultStatus.Success;
                    }
                    else
                        result = ResultStatus.RecordNotFound;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public static ResultStatus DeleteUser(int userId)
        {
            ResultStatus result = ResultStatus.Null;

            try
            {
                using (SC_DBDataContext dc = new SC_DBDataContext())
                {
                    var dbUser = dc.Users.SingleOrDefault(u => u.UserId == userId);

                    if (dbUser != null)
                    {
                        dc.Users.DeleteOnSubmit(dbUser);
                        dc.SubmitChanges();

                        result = ResultStatus.Success;
                    }
                    else
                        result = ResultStatus.RecordNotFound;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public enum ResultStatus
        {
            Null = 0,
            RecordNotFound = -1,
            Success = 100
        }
    }
}