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
        public List<TenderViewModel> TenderEntries()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var dbTender = db.Tender.ToList();
                    List<TenderViewModel> tenders = new List<TenderViewModel>();

                    foreach (var tender in dbTender)
                    {
                        var result = new TenderViewModel
                        {
                            Id = tender.Id,
                            CreatedDate = tender.CreatedDate.ToShortDateString(),
                            UserId = tender.User.Id,
                            TenderNo = tender.TenderNo,
                            OpenDate = tender.OpenDate.ToString(),
                            CloseDate = tender.CloseDate.ToString(),
                            StatusId = tender.Status.Id
                            //StatusId = dbTender.FirstOrDefault(role => role.Id == tender.Id)
                        };
                        tenders.Add(result);
                    }
                    return tenders;
                }
                //using (db = new ApplicationDbContext())
                //{
                //  var tenders = db.Tender.ToList();
                //var tenders = db.Tender
                //    .Include(x => x.Stock)
                //    .Include(x => x.Stock.Car.Manufacturer)
                //    .Include(x => x.Stock.Car)
                //    .Include(x => x.Stock.Location)
                //    .Select(x => new StockDTO
                //    {
                //        Id = x.Stock.Id,
                //        Comments = x.Stock.Comments,
                //        ModelLineId = x.Stock.ModelLineId,
                //        IsSold = x.Stock.IsSold,
                //        LocationId = x.Stock.LocationId,
                //        Mileage = x.Stock.Mileage,
                //        Price = x.Stock.Price,
                //        RegNo = x.Stock.RegNo,
                //        CarModel = x.Stock.Car.ModelName
                //    })
                //    .ToList();
                // return tenders;
                //}
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
                        CloseDate = tender.CloseDate,
                        CreatedDate = tender.CreatedDate,
                        OpenDate = tender.OpenDate,
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

        public void EditTender(Tender editedTender)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    var editedSingleId = db.Tender.FirstOrDefault(x => x.Id == editedTender.Id);
                    editedSingleId.Id = editedTender.Id;
                    editedSingleId.UserId = editedTender.UserId;
                    editedSingleId.CreatedDate = editedTender.CreatedDate;
                    editedSingleId.OpenDate = editedTender.OpenDate;
                    editedSingleId.CloseDate = editedTender.CloseDate;
                    editedSingleId.StatusId = editedTender.StatusId;
                    editedSingleId.TenderNo = editedTender.TenderNo;
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
                    var tenders = db.Tender.Include(x => x.User).ToList();
                    var tenderUser = db.TenderUser.Where(u => u.UserId == userId).ToList();
                    List<HomeTableDTO> tenderList = new List<HomeTableDTO>();
                    if (userRole != true)
                    { 
                         foreach(var tU in tenderUser)
                        {
                            tenders.RemoveAll(t => t.Id != tU.TenderId);
                        }
                    }
                    foreach (var tender in tenders)
                    {
                        var a = new HomeTableDTO
                        {
                            TenderNo = tender.TenderNo,
                            Dealer = tender.User.DealerName,
                            DealerName = tender.User.FirstName + " " + tender.User.LastName,
                            OpenDate = tender.OpenDate.ToString().Substring(0, 10),
                            CloseDate = tender.CloseDate.ToString().Substring(0, 10)

                        };
                        tenderList.Add(a);
                    }
                    return tenderList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public HomeTableDTO TenderInfo(string Id)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    HomeTableDTO tenderList;
                    var tender = db.Tender.Include(x => x.User).First(x => x.TenderNo == Id);
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
        public List<TenderCarsDTO> GetTenderCars(int Id)
        {
            try
            {
                using (db= new ApplicationDbContext())
                {
                    List<TenderCarsDTO> cars = new List<TenderCarsDTO>();
                    var carsFromDb = db.TenderStock.Include(x => x.Stock).Include(x => x.Stock.Car).Include(x => x.Stock.Car.Manufacturer).Where(x => x.TenderId == Id).ToList();
                    foreach(var car in carsFromDb)
                    {
                        var carDB = new TenderCarsDTO {
                            Id = car.Stock.Id,
                            RegNo = car.Stock.RegNo,
                            Year = car.Stock.Year,
                            Make = car.Stock.Car.Manufacturer.ManufacturerName,
                            CarLine = car.Stock.Car.ModelName,
                            Model = car.Stock.Car.ModelNo,
                            Mileage = car.Stock.Mileage,
                            Comments = car.Stock.Comments
                        };
                        cars.Add(carDB);
                    }
                    return cars;
                }
               
                
            }
            catch(Exception)
            {
                throw;
            }
        }
        public List<TenderBids> GetBids (int Id, string userId)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<TenderBids> bids = new List<TenderBids>();
                    var bidsFromDb = db.Bid.Include(x=> x.User.UserId).Where(x => x.TenderStockId == Id).Where(x => x.User.UserId == userId).ToList();
                    foreach(var bid in bidsFromDb)
                    {
                        var bidDB = new TenderBids
                        {
                            Id = bid.Id,
                            TenderUserId = bid.TenderUserId,
                            TenderStockId = bid.TenderStockId,
                            Price = bid.Price,
                            IsWiningPrice = bid.IsWinningPrice
                        };
                        bids.Add(bidDB);
                    }
                    return bids;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
