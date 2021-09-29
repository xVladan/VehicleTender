using BusinessLogic.DataTransferObjects.MobileDTO;
using DataAccessLayer_DAL;
using DataAccessLayer_DAL.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class MobileLogic
    {
        private ApplicationDbContext db;


        // User methods
        public List<UserMobileDTO> GetUsersList()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var dbUsers = db.Users.ToList();
                    List<UserMobileDTO> users = new List<UserMobileDTO>();

                    foreach(var user in dbUsers)
                    {
                        var roleId = user.Roles.FirstOrDefault(role => role.UserId == user.Id).RoleId;
                        var roleName = db.Roles.FirstOrDefault(role => role.Id == roleId).Name;
                        var mobileUser = new UserMobileDTO
                        {
                            Id = user.Id,
                            Email = user.Email,
                            LocationId = user.LocationId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            CompanyName = user.DealerName,
                            isActive = user.isActive,
                            RoleId = roleId,
                            RoleName = roleName
                        };
                        users.Add(mobileUser);
                    }
                    return users;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }


        public UserMobileDTO GetUserByEmail(string email)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var userByEmail = db.Users
                        .Select(user => new UserMobileDTO
                        {
                            Id = user.Id,
                            Email = user.Email,
                            LocationId = user.LocationId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            CompanyName = user.DealerName,
                            isActive = user.isActive,
                            RoleId = user.Roles.FirstOrDefault(role => role.UserId == user.Id).RoleId
                        })
                        .FirstOrDefault(user => user.Email == email);

                    var roleName = db.Roles.FirstOrDefault(role => role.Id == userByEmail.RoleId).Name;
                    userByEmail.RoleName = roleName;
                    return userByEmail;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<AspNetRolesMeta> AllRoles()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var roles = db.Roles
                        .Select(role => new AspNetRolesMeta
                        {
                            Id = role.Id,
                            Name = role.Name
                        })
                        .ToList();
                    return roles;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        // Manufacturer methods

        public Manufacturer SaveManufacturerInDb(Manufacturer manufacturerData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var manufacturer = db.Manufacturer.Add(manufacturerData);
                    db.SaveChanges();
                    return manufacturer;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // Car Model methods 
        public List<CarModelMobileDTO> GetAllCars()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var carModels = db.CarModel
                        .Select(car => new CarModelMobileDTO
                        {
                            Id = car.Id,
                            ModelName = car.ModelName,
                            ModelNo = car.ModelNo,
                            ManufacturerId = car.ManufacturerId
                        })
                        .ToList();

                    return carModels;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public CarModel AddCar(CarModel carData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var savedCar = db.CarModel.Add(carData);
                    db.SaveChanges();
                    return savedCar;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        // Stock methods

        public List<StockInfoMobileDTO> GetAllStocks()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var stocks = db.StockInfo
                        .Select(stock => new StockInfoMobileDTO
                        {
                            Id = stock.Id,
                            ModelLineId = stock.ModelLineId,
                            Mileage = stock.Mileage,
                            Price = stock.Price,
                            Comments = stock.Comments,
                            LocationId = stock.LocationId,
                            RegNo = stock.RegNo,
                            IsSold = stock.IsSold,
                            SaledDate = stock.SaledDate
                        })
                        .ToList();

                    return stocks;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public StockInfo SaveStockInDb(StockInfo stockData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var stock = db.StockInfo.Add(stockData);
                    db.SaveChanges();
                    return stock;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        // Location methods 

        public Location SaveLocationInDB(Location location)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var savedLocation = db.Location.Add(location);
                    db.SaveChanges();
                    return savedLocation;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        // Tenders methods 

        public List<TenderMobileDTO> GetAllTenders()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tenders = db.Tender
                        .Select(tender => new TenderMobileDTO
                        {
                            Id = tender.Id,
                            CreatedDate = tender.CreatedDate,
                            UserId = tender.UserId,
                            TenderNo = tender.TenderNo,
                            OpenDate = tender.OpenDate,
                            CloseDate = tender.CloseDate,
                            StatusId = tender.StatusId
                        })
                        .ToList();
                    return tenders;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public Tender SaveTenderInDb(Tender tenderData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tender = db.Tender.Add(tenderData);
                    db.SaveChanges();
                    return tender;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public List<TenderStatus> GetTenderStatuses()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tS = db.TenderStatus.ToList();
                    return tS;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderStatus AddTenderStatus(TenderStatus tenderStatusData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var savedTenderStatus = db.TenderStatus.Add(tenderStatusData);
                    db.SaveChanges();
                    return savedTenderStatus;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public List<TenderStock> AllTenderStocks(string tenderIdString)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    int tenderId = Int32.Parse(tenderIdString);
                    var tenderStocks = db.TenderStock.Where(tStock => tStock.isDeleted == false && tStock.TenderId == tenderId).ToList();
                    return tenderStocks;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderStock AddTenderStock(TenderStock data)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var savedData = db.TenderStock.Add(data);
                    return savedData;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public List<TenderUser> AllTenderUsers(string tenderIdString)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    int tenderId = Int32.Parse(tenderIdString);
                    var tenderUsers = db.TenderUser.Where(tUser => tUser.TenderId == tenderId).ToList();
                    return tenderUsers;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderUser AddTenderUser(TenderUser tenderUserData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var savedData = db.TenderUser.Add(tenderUserData);
                    return savedData;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

    }
}
