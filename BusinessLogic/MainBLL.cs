using DataAccessLayer_DAL;
using DataAccessLayer_DAL.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using BusinessLogic.DataTransferObjects;

namespace BusinessLogic
{
    public class MainBLL
    {
        private ApplicationDbContext db;
        // User Methods

        public List<AspNetUsersMeta> GetUsersList()
        {
            using (db = new ApplicationDbContext())
            {
                var dbUsers = db.Users.ToList();
                List<AspNetUsersMeta> users = new List<AspNetUsersMeta>();

                foreach (var user in dbUsers)
                {
                    var result = new AspNetUsersMeta
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        FullName = user.FirstName + " " + user.LastName,
                        DealerName = user.DealerName,
                        Email = user.Email,
                        isActive = user.isActive,
                        RoleId = user.Roles.FirstOrDefault(role => role.UserId == user.Id).RoleId
                    };
                    users.Add(result);
                }
                return users;
            }
        }

        public List<AspNetRolesMeta> GetRoles()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<AspNetRolesMeta> roles = new List<AspNetRolesMeta>();
                    var dbRoles = db.Roles.ToList();

                    foreach (var role in dbRoles)
                    {
                        roles.Add(new AspNetRolesMeta
                        {
                            Id = role.Id,
                            Name = role.Name
                        });
                    }
                    return roles;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AspNetRolesMeta EditDbUser(AspNetUsersMeta user)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var userById = db.Users.FirstOrDefault(x => x.Id == user.Id);
                    var roles = db.Roles.ToList();
                    var roleById = db.Roles
                        .Select(role => new AspNetRolesMeta
                        {
                            Id = role.Id,
                            Name = role.Name
                        })
                        .FirstOrDefault(role => role.Id == user.RoleId);
                    if (roleById.Id == user.RoleId)
                    {
                        userById.FirstName = user.FirstName;
                        userById.LastName = user.LastName;
                        userById.UserName = user.Email;
                        userById.DealerName = user.DealerName;
                        userById.Email = user.Email;
                        userById.isActive = user.isActive;
                        db.SaveChanges();
                        return roleById;
                    }
                    throw new Exception("sadasd");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteUserFromDb(string Id)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var userById = db.Users.FirstOrDefault(x => x.Id == Id);
                    userById.isActive = false;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Stock Methods
        public List<Manufacturer> GetManufacturers()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var manufacturers = db.Manufacturer.ToList();
                    return manufacturers;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GenericDropdown> ManufacturerDropdown()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var listFormDb = db.Manufacturer
                .Select(p => new GenericDropdown()
                {
                    id = p.Id,
                    text = p.ManufacturerName,
                }).OrderBy(x => x.id).ToList();

                    return listFormDb;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void SaveManufacturerInDB(Manufacturer PostData)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.Manufacturer.Add(PostData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void EditDbManufacturer(int Id, string MFName)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var manufactById = db.Manufacturer.FirstOrDefault(x => x.Id == Id);
                    manufactById.ManufacturerName = MFName;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteManufacturer(int Id)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var manufactById = db.Manufacturer.FirstOrDefault(x => x.Id == Id);
                    db.Manufacturer.Remove(manufactById);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Methods for Car Model

        public List<CarModel> CarEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var carmodels = db.CarModel.Include(x => x.Manufacturer).ToList();
                    return carmodels;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GenericDropdown> CarModelDropdown()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var listFormDb = db.CarModel.Include(x => x.Manufacturer)
                .Select(p => new GenericDropdown()
                {
                    id = p.Id,
                    text = p.Manufacturer.ManufacturerName + " " + p.ModelName + " " + p.ModelNo,
                }).OrderBy(x => x.id).ToList();

                    return listFormDb;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void CreateCar(CarModel PostData)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.CarModel.Add(PostData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditCar(CarModel carModel)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var modelById = db.CarModel.FirstOrDefault(x => x.Id == carModel.Id);
                    modelById.ModelName = carModel.ModelName;
                    modelById.ModelNo = carModel.ModelNo;
                    modelById.ManufacturerId = carModel.ManufacturerId;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCar(int Id)
        {
            using (db = new ApplicationDbContext())
            {
                var modelById = db.CarModel.FirstOrDefault(x => x.Id == Id);
                db.CarModel.Remove(modelById);
                db.SaveChanges();
            }
        }

        //Stock Info

        public List<StockDTO> StockEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var stocks = db.StockInfo
                        .Include(x => x.Car.Manufacturer)
                        .Include(x => x.Car)
                        .Include(x => x.Location)
                        .Select(x => new StockDTO
                        {
                            Id = x.Id,
                            Comments = x.Comments,
                            IsSold = x.IsSold,
                            Mileage = x.Mileage,
                            Price = x.Price,
                            RegNo = x.RegNo,
                            CarModel = x.Car.ModelName,
                            LocationId = x.LocationId,
                            ModelLineId = x.ModelLineId,
                            Year = x.Year
                        })
                        .ToList();

                    return stocks;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateStock(StockInfo PostData)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.StockInfo.Add(PostData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditStock(StockInfo stockModel)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var modelById = db.StockInfo.FirstOrDefault(x => x.Id == stockModel.Id);
                    modelById.ModelLineId = stockModel.ModelLineId;
                    modelById.Mileage = stockModel.Mileage;
                    modelById.Price = stockModel.Price;
                    modelById.Comments = stockModel.Comments;
                    modelById.LocationId = stockModel.LocationId;
                    modelById.RegNo = stockModel.RegNo;
                    modelById.IsSold = stockModel.IsSold;
                    modelById.Year = stockModel.Year;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteStock(int Id)
        {
            using (db = new ApplicationDbContext())
            {
                var modelById = db.StockInfo.FirstOrDefault(x => x.Id == Id);
                db.StockInfo.Remove(modelById);
                db.SaveChanges();
            }
        }

        public List<StockDTO> StockDropdown()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var listFormDb = db.StockInfo
                        .Include(x => x.Car)
                        .Include(x => x.Location)
                        .Include(x => x.Car.Manufacturer)
                    .Select(p => new StockDTO
                    {
                        Id = p.Id,
                        Comments = p.Comments,
                        IsSold = p.IsSold,
                        Location = p.Location.City,
                        Manufacturer = p.Car.Manufacturer.ManufacturerName,
                        ModelNo = p.Car.ModelNo,
                        Mileage = p.Mileage,
                        Price = p.Price,
                        RegNo = p.RegNo,
                        CarModel = p.Car.ModelName,
                        Year = p.Year,
                        FullCarName = p.Car.Manufacturer.ManufacturerName + " " + p.Car.ModelName
                    }).OrderBy(x => x.Id).ToList();
                    return listFormDb;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //LOCATION
        public List<Location> LocationEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var locmodels = db.Location.ToList();
                    return locmodels;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveLocationInDB(Location loc)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.Location.Add(loc);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditLocationInDB(int Id, Location LocName)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var locById = db.Location.FirstOrDefault(x => x.Id == Id);
                    locById.City = LocName.City;
                    locById.ZipCode = LocName.ZipCode;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void DeleteLocationByID(int Id)
        {
            using (db = new ApplicationDbContext())
            {
                var loclById = db.Location.FirstOrDefault(x => x.Id == Id);
                db.Location.Remove(loclById);
                db.SaveChanges();
            }
        }

        public List<GenericDropdown> LocationDropdown()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var listFormDb = db.Location
                .Select(p => new GenericDropdown()
                {
                    id = p.Id,
                    text = p.City +  " " + p.ZipCode,
                }).OrderBy(x => x.id).ToList();

                    return listFormDb;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        // TenderStatus


        public List<TenderStatus> TenderStatusEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var tenderStatus = db.TenderStatus.ToList();
                    return tenderStatus;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveTenderStatusInDb(TenderStatus tenderStatusData)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.TenderStatus.Add(tenderStatusData);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditTenderStatus(int Id, TenderStatus tenderStatus)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var tsById = db.TenderStatus.FirstOrDefault(x => x.Id == Id);
                    tsById.Type = tenderStatus.Type;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteTenderStatus(int Id)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var item = db.TenderStatus.FirstOrDefault(x => x.Id == Id);
                    db.TenderStatus.Remove(item);
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        ///////////////////////////// 
        //    TENDER
        ///////////////////////////
        public List<TenderDTO> TenderEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var dbTender = db.Tender.ToList();
                    List<TenderDTO> tenders = new List<TenderDTO>();

                    foreach (var tender in dbTender)
                    {
                        var result = new TenderDTO
                        {
                            Id = tender.Id,
                            CreatedDate = tender.CreatedDate.ToString(),
                            UserId = tender.User.Id,
                            TenderNo = tender.TenderNo,
                            OpenDate = tender.OpenDate.ToString(),
                            CloseDate = tender.CloseDate.ToString(),
                            StatusId = tender.Status.Id,
                            TenderStockId = db.TenderStock.Where(x => x.TenderId == tender.Id && x.isDeleted == false).Select(x => x.StockId).ToList(),
                            TenderUserId = db.TenderUser.Where(x => x.TenderId == tender.Id && x.isDeleted == false).Select(x => x.UserId).ToList(),
                            //StatusId = dbTender.FirstOrDefault(role => role.Id == tender.Id)
                        };
                        tenders.Add(result);
                    }
                    return tenders;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTender(TenderDTO tender)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    Tender t = new Tender
                    {
                        CloseDate = DateTime.Parse(tender.CloseDate),
                        CreatedDate = DateTime.Parse(tender.CreatedDate),
                        OpenDate = DateTime.Parse(tender.OpenDate),
                        StatusId = tender.StatusId,
                        TenderNo = tender.TenderNo,
                        UserId = tender.UserId,
                    };

                  var savedTender =  db.Tender.Add(t);
                  foreach(var tS in tender.TenderStockId)
                    {
                        TenderStock tenderStock = new TenderStock
                        {
                            TenderId = savedTender.Id,
                            StockId = tS,
                        };
                        db.TenderStock.Add(tenderStock);
                    };

                    foreach(var tU in tender.TenderUserId)
                    {
                        TenderUser tenderUser = new TenderUser
                        {
                            TenderId = savedTender.Id,
                            UserId = tU,
                        };
                        db.TenderUser.Add(tenderUser);
                    };
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EditTender(TenderDTO editedTender)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var TenderStockDB = db.TenderStock.Where(x => x.TenderId == editedTender.Id);
                    var TenderUserDB = db.TenderUser.Where(x => x.TenderId == editedTender.Id);

                    foreach (var stock in TenderStockDB)
                    {
                        stock.isDeleted = true;
                    }

                    foreach (var user in TenderUserDB)
                    {
                        user.isDeleted = true;
                    }

                    var editedSingleId = db.Tender.FirstOrDefault(x => x.Id == editedTender.Id);
                    editedSingleId.Id = editedTender.Id;
                    editedSingleId.UserId = editedTender.UserId;
                    editedSingleId.CreatedDate = DateTime.Parse(editedTender.CreatedDate);
                    editedSingleId.OpenDate = DateTime.Parse(editedTender.OpenDate);
                    editedSingleId.CloseDate = DateTime.Parse(editedTender.CloseDate);
                    editedSingleId.StatusId = editedTender.StatusId;
                    editedSingleId.TenderNo = editedTender.TenderNo;

                    foreach (var tS in editedTender.TenderStockId)
                    {
                        if (TenderStockDB.Where(x => x.StockId == tS).FirstOrDefault() != null)
                        {
                            TenderStockDB.Where(x => x.StockId == tS).First().isDeleted = false;
                        }
                        else
                        {
                            TenderStock tenderStock = new TenderStock
                            {
                                TenderId = editedSingleId.Id,
                                StockId = tS,
                            };
                            db.TenderStock.Add(tenderStock);
                        }

                    };
                    foreach (var tU in editedTender.TenderUserId)
                    {
                        if (TenderUserDB.Where(x => x.UserId == tU).FirstOrDefault() != null)
                        {
                            TenderUserDB.Where(x => x.UserId == tU).First().isDeleted = false;
                        }
                        else
                        {
                            TenderUser tenderUser = new TenderUser
                            {
                                TenderId = editedSingleId.Id,
                                UserId = tU,
                            };
                            db.TenderUser.Add(tenderUser);
                        }

                    };
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }








        ///////
        ///tender 
        ///
        ////////////
        public List<TenderStatus> GetTenderStatusType()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<TenderStatus> roles = new List<TenderStatus>();
                    var dbTenderStatus = db.TenderStatus.ToList();

                    foreach (var role in dbTenderStatus)
                    {
                        roles.Add(new TenderStatus
                        {
                            Id = role.Id,
                            Type = role.Type
                        });
                    }
                    return roles;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<HomeTableDTO> HomeTable(string userId, bool userRole)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var tenders = db.Tender
                        .Include(x => x.User)
                        .Select(tender => new HomeTableDTO 
                        {
                            Id = tender.Id,
                            TenderNo = tender.TenderNo,
                            Dealer = tender.User.DealerName,
                            DealerName = tender.User.FirstName + " " + tender.User.LastName,
                            OpenDate = tender.OpenDate.ToString().Substring(0, 10),
                            CloseDate = tender.CloseDate.ToString().Substring(0, 10)
                        })
                        .ToList();
                    var tenderUser = db.TenderUser.Where(u => u.UserId == userId && u.isDeleted == false).ToList();
                    List<HomeTableDTO> tenderList = new List<HomeTableDTO>();
                    if (userRole != true)
                    { 
                         foreach(var tU in tenderUser)
                        {
                            foreach(var t in tenders)
                            {
                                if(t.Id == tU.TenderId)
                                {
                                    tenderList.Add(t);
                                }
                            }
                        }
                    }
                    else
                    {
                        return tenders;
                    }
                    return tenderList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public HomeTableDTO TenderInfo(int Id)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    HomeTableDTO tenderList;
                    var tender = db.Tender.Include(x => x.User).First(x => x.Id == Id);
                    tenderList = new HomeTableDTO
                    {
                        Id = tender.Id,
                        TenderNo = tender.TenderNo,
                        Dealer = tender.User.DealerName,
                        DealerName = tender.User.FirstName + " " + tender.User.LastName,
                        OpenDate = tender.OpenDate.ToString().Substring(0, 10),
                        CloseDate = tender.CloseDate.ToString().Substring(0, 10)

                    };
                    return tenderList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TenderCarsAndBidsDTO> GetTenderCars(int Id, string userId)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<TenderCarsAndBidsDTO> cars = new List<TenderCarsAndBidsDTO>();
                    if(userId != null)
                    {
                        var carsFromDb = db.TenderStock
                            .Include(x => x.Stock)
                            .Include(x => x.Stock.Car)
                            .Include(x => x.Stock.Car.Manufacturer)
                            .Where(x => x.TenderId == Id && x.isDeleted == false)
                            .ToList();
                        var bidsFromDb = db.Bid
                            .Include(x => x.Stock)
                            .Include(x => x.User)
                            .Include(x => x.User.User)
                            .Where(x => x.User.User.Id == userId)
                            .Where(x => x.Stock.TenderId == Id)
                            .Where(x => x.isActive == true)
                            .ToList();
                        foreach (var car in carsFromDb)
                        {
                            var carDB = new TenderCarsAndBidsDTO
                            {
                                Id = car.StockId,
                                TenderStockId = car.Id,
                                RegNo = car.Stock.RegNo,
                                Year = car.Stock.Year,
                                Make = car.Stock.Car.Manufacturer.ManufacturerName,
                                CarLine = car.Stock.Car.ModelName,
                                Model = car.Stock.Car.ModelNo,
                                Mileage = car.Stock.Mileage,
                                Comments = car.Stock.Comments
                            };
                            foreach (var bid in bidsFromDb)
                            {
                                if (car.Id == bid.TenderStockId)
                                {
                                    carDB.BidPrice = bid.Price;
                                    carDB.IdBid = bid.Id;
                                }
                            }
                            cars.Add(carDB);
                        }
                        return cars;
                    } else
                    {
                        var stocks = db.TenderStock
                            .Include(x => x.Stock)
                            .Include(x => x.Stock.Car)
                            .Include(x => x.Stock.Car.Manufacturer)
                            .Where(x => x.TenderId == Id)
                            .Select(car => new TenderCarsAndBidsDTO
                            {
                                Id = car.StockId,
                                TenderStockId = car.Id,
                                RegNo = car.Stock.RegNo,
                                Year = car.Stock.Year,
                                Make = car.Stock.Car.Manufacturer.ManufacturerName,
                                CarLine = car.Stock.Car.ModelName,
                                Model = car.Stock.Car.ModelNo,
                                Mileage = car.Stock.Mileage,
                                Comments = car.Stock.Comments
                            })
                            .ToList();
                        return stocks;
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void AddBid(int TenderStockId, double Price, string userId, int TenderId)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var tendeUserId = db.TenderUser.Where(x => x.TenderId == TenderId).Where(x => x.UserId == userId).First();
                    var userBid = db.Bid.Where(x => x.TenderUserId == tendeUserId.Id).Where(x => x.TenderStockId == TenderStockId);
                    foreach (var bid in userBid)
                    {
                        bid.isActive = false;
                    }
                    Bid newBid = new Bid
                    {
                        TenderUserId = tendeUserId.Id,
                        TenderStockId = TenderStockId,
                        Price = Price,
                        IsWinningPrice = false,
                        isActive = true
                    };
                    db.Bid.Add(newBid);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveWinnerBid(int Id, int tenderId, int stockId)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var bids = db.Bid
                        .Include(bid => bid.Stock)
                        .Where(bid => bid.Stock.TenderId == tenderId && bid.Stock.StockId == stockId && bid.isActive == true)
                        .ToList();

                    foreach (var bid in bids)
                    {
                        if(bid.Id == Id)
                        {
                            bid.IsWinningPrice = true;

                        } else
                        {
                            bid.IsWinningPrice = false;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public List<BidDTO> BidsByTenderIdAndStockId(int tenderId, int stockId)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var bids = db.Bid
                        .Include(x => x.Stock)
                        .Include(x => x.Stock.Stock)
                        .Include(x => x.User)
                        .Include(x => x.User.User)
                        .Where(x => x.Stock.TenderId == tenderId && x.Stock.StockId == stockId)
                        .Where(x => x.isActive == true)
                        .Select(bid => new BidDTO
                        {
                            Id = bid.Id,
                            Price = bid.Price,
                            IsWinningPrice = bid.IsWinningPrice,
                            BidderName = bid.User.User.FirstName,
                            StockId = bid.Stock.StockId
                        })
                        .ToList();
                    return bids;
                }

            }
            catch(Exception error)
            {
                throw error;

            }
        }
    }
}
