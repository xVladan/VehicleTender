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
                    if(userByEmail != null)
                    {
                        var roleName = db.Roles.FirstOrDefault(role => role.Id == userByEmail.RoleId).Name;
                        userByEmail.RoleName = roleName;
                        return userByEmail;
                    }
                    return null;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public UserMobileDTO EditUser(UserMobileDTO editData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var userRole = db.Roles.FirstOrDefault(role => role.Id == editData.RoleId);
                    var userById = db.Users.FirstOrDefault(user => user.Id == editData.Id);
                    
                    if(userById != null)
                    {
                        userById.FirstName = editData.FirstName;
                        userById.LastName = editData.LastName;
                        userById.UserName = editData.Email;
                        userById.DealerName = editData.CompanyName;
                        userById.Email = editData.Email;
                        userById.isActive = editData.isActive;
                        userById.LocationId = editData.LocationId;
                        userById.PhoneNumber = editData.PhoneNumber;
                        editData.RoleName = userRole.Name;
                        db.SaveChanges();
                        return editData;
                    }
                    return null;
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

        public CarModelMobileDTO AddCar(CarModelMobileDTO carData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    CarModel car = new CarModel
                    {
                        ModelName = carData.ModelName,
                        ModelNo = carData.ModelNo,
                        ManufacturerId = carData.ManufacturerId
                    };
                    var savedCar = db.CarModel.Add(car);
                    db.SaveChanges();
                    carData.Id = savedCar.Id;
                    return carData;
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
                            SaledDate = stock.SaledDate,
                            Year = stock.Year
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

        public StockInfoMobileDTO SaveStockInDb(StockInfoMobileDTO stockData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    StockInfo stockInfo = new StockInfo
                    {
                        ModelLineId = stockData.ModelLineId,
                        Mileage = stockData.Mileage,
                        Price = stockData.Price,
                        Comments = stockData.Comments,
                        LocationId = stockData.LocationId,
                        RegNo = stockData.RegNo,
                        IsSold = stockData.IsSold,
                        SaledDate = stockData.SaledDate,
                        Year = stockData.Year
                    };
                    var stock = db.StockInfo.Add(stockInfo);
                    db.SaveChanges();
                    stockData.Id = stock.Id;
                    return stockData;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public StockInfoMobileDTO EditStock(StockInfoMobileDTO stockData)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var stockById = db.StockInfo.FirstOrDefault(stock => stock.Id == stockData.Id);
                    stockById.IsSold = stockData.IsSold;
                    db.SaveChanges();
                    return new StockInfoMobileDTO {
                        Id = stockById.Id,
                        ModelLineId = stockById.ModelLineId,
                        Mileage = stockById.Mileage,
                        Price = stockById.Price,
                        Comments = stockById.Comments,
                        LocationId = stockById.LocationId,
                        RegNo = stockById.RegNo,
                        IsSold = stockById.IsSold,
                        SaledDate = stockById.SaledDate,
                        Year = stockById.Year
                    };
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

        public TenderMobileDTO SaveTenderInDb(TenderMobileDTO tenderData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    Tender tender = new Tender
                    {
                        CreatedDate = tenderData.CreatedDate,
                        UserId = tenderData.UserId,
                        TenderNo = tenderData.TenderNo,
                        OpenDate = tenderData.OpenDate,
                        CloseDate = tenderData.CloseDate,
                        StatusId = tenderData.StatusId
                    };
                    var savedTender = db.Tender.Add(tender);
                    db.SaveChanges();
                    tenderData.Id = savedTender.Id;
                    return tenderData;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderMobileDTO EditTender(int tenderId, int statusId)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tenderById = db.Tender.FirstOrDefault(tender => tender.Id == tenderId);
                    tenderById.StatusId = statusId;
                    db.SaveChanges();
                    return new TenderMobileDTO
                    {
                        Id = tenderById.Id,
                        CreatedDate = tenderById.CreatedDate,
                        UserId = tenderById.UserId,
                        TenderNo = tenderById.TenderNo,
                        OpenDate = tenderById.OpenDate,
                        CloseDate = tenderById.CloseDate,
                        StatusId = tenderById.StatusId
                    };
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

        public List<TenderStockMobileDTO> AllTenderStocks()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tenderStocks = db.TenderStock
                        .Select(tStock => new TenderStockMobileDTO
                        {
                            Id = tStock.Id,
                            TenderId = tStock.TenderId,
                            StockId = tStock.StockId,
                            isDeleted = tStock.isDeleted,
                            SaleDate = tStock.SaleDate
                        })
                        .ToList();
                    return tenderStocks;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderStockMobileDTO AddTenderStock(TenderStockMobileDTO data)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var dbTenderStocks = db.TenderStock.Where(tS => tS.StockId == data.StockId && tS.TenderId == data.TenderId).ToList();
                    if(dbTenderStocks.Count != 0)
                    {
                        throw new Exception("Stock already exists!");
                    }
                    TenderStock tenderStock = new TenderStock
                    {
                        StockId = data.StockId,
                        TenderId = data.TenderId,
                        isDeleted = data.isDeleted,
                        SaleDate = data.SaleDate
                    };
                    var savedData = db.TenderStock.Add(tenderStock);
                    db.SaveChanges();
 
                    return new TenderStockMobileDTO 
                    {
                        Id = savedData.Id,
                        StockId = savedData.StockId,
                        TenderId = savedData.TenderId,
                        isDeleted = savedData.isDeleted,
                        SaleDate = savedData.SaleDate
                    };
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderStockMobileDTO EditTenderStock(TenderStockMobileDTO data)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tenderStockById = db.TenderStock.FirstOrDefault(tS => tS.Id == data.Id);
                    tenderStockById.isDeleted = data.isDeleted;
                    tenderStockById.SaleDate = data.SaleDate;
                    db.SaveChanges();
                    TenderStockMobileDTO savedData = new TenderStockMobileDTO 
                    {
                        Id = tenderStockById.Id,
                        StockId = tenderStockById.StockId,
                        TenderId = tenderStockById.TenderId,
                        SaleDate = tenderStockById.SaleDate,
                        isDeleted = tenderStockById.isDeleted
                    };
                    return savedData;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<TenderUserMobileDTO> AllTenderUsers()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var tenderUsers = db.TenderUser
                        .Select(tUser => new TenderUserMobileDTO
                        {
                            Id = tUser.Id,
                            TenderId = tUser.TenderId,
                            UserId = tUser.UserId
                        })
                        .ToList();
                    return tenderUsers;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public TenderUserMobileDTO AddTenderUser(TenderUserMobileDTO tenderUserData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    TenderUser tenderUser = new TenderUser
                    {
                        TenderId = tenderUserData.TenderId,
                        UserId = tenderUserData.UserId
                    };
                    var savedData = db.TenderUser.Add(tenderUser);
                    db.SaveChanges();
                    tenderUserData.Id = savedData.Id;
                    return tenderUserData;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public List<BidMobileDTO> AllBids()
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var allBids = db.Bid
                        .Select(bid => new BidMobileDTO
                        {
                            Id = bid.Id,
                            TenderStockId = bid.TenderStockId,
                            TenderUserId = bid.TenderUserId,
                            IsWinningPrice = bid.IsWinningPrice,
                            isActive = bid.isActive,
                            Price = bid.Price
                        })
                        .ToList();
                    return allBids;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public BidMobileDTO SaveBid(BidMobileDTO bidData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var bid = new Bid
                    {
                        TenderStockId = bidData.TenderStockId,
                        TenderUserId = bidData.TenderUserId,
                        IsWinningPrice = bidData.IsWinningPrice,
                        isActive = true,
                        Price = bidData.Price
                    };
                    var bids = db.Bid.Where(b => b.TenderStockId == bid.TenderStockId && b.TenderUserId == bid.TenderUserId).ToList();
                    if(bids.Count != 0)
                    {
                        foreach(var b in bids)
                        {
                            b.isActive = false;
                        }
                    }
                    var storedBid = db.Bid.Add(bid);
                    db.SaveChanges();
                    return new BidMobileDTO {
                        Id = storedBid.Id,
                        TenderStockId = storedBid.TenderStockId,
                        TenderUserId = storedBid.TenderUserId,
                        Price = storedBid.Price,
                        IsWinningPrice = storedBid.IsWinningPrice,
                        isActive = storedBid.isActive
                    };
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public BidMobileDTO SetWiningBid(BidMobileDTO bidData)
        {
            try
            {
                using(db = new ApplicationDbContext())
                {
                    var bid = db.Bid.FirstOrDefault(b => b.TenderStockId == bidData.TenderStockId && b.TenderUserId == bidData.TenderUserId && b.isActive == true);
                    bid.IsWinningPrice = true;
                    db.SaveChanges();
                    return new BidMobileDTO
                    {
                        Id = bid.Id,
                        TenderStockId = bid.TenderStockId,
                        TenderUserId = bid.TenderUserId,
                        Price = bid.Price,
                        IsWinningPrice = bid.IsWinningPrice,
                        isActive = bid.isActive
                    };
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
