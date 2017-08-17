using PonziBussinessLogic.Interface;
using PonziRepostiory;
using PonziRepostiory.Implementation;
using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic.Implementation
{
    public class PonziAdminLogin: IPonziAdminLogic
    {
        IUnitofWork _unitofWork = new UnitofWork();

        public BusinessMessage<TransactionDetail> GetSingleTransactionDetail(int TransId)
        {
            try
            {
                var trans = _unitofWork.TransactionDetail.GetSingle(TransId);
                BusinessMessage<TransactionDetail> msg = new BusinessMessage<TransactionDetail>();
                msg.ResponseCode = ResponseCode.OK;
                msg.Result = trans;
                msg.Message = "Selection done";
                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<TransactionDetail> msg = new BusinessMessage<TransactionDetail>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = null;
                return msg;
            }
        }
        public BusinessMessage<bool> EditTransactionDetail(TransactionDetail Trans)
        {
            try
            {
                _unitofWork.TransactionDetail.Edit(Trans,Trans.Id);
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                    if (_unitofWork.SaveChanges() > 0)
                    {
                        msg.Message = string.Format("You have succesfully edited transaction from bucket");
                        msg.ResponseCode = ResponseCode.OK;
                        msg.Result = true;
                    }
                    else
                    {
                        msg.Message = string.Format("Attempt to edit {0} to transaction bucket failed", Trans.Description);
                        msg.ResponseCode = ResponseCode.NotOK;
                        msg.Result = false;
                    }
                
           
                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                return msg;
            }
        }
        public BusinessMessage<bool> CreateNewTransactionDetail(TransactionDetail Trans)
        {
            try
            {
             
                _unitofWork.TransactionDetail.Add(Trans);
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                if (_unitofWork.TransactionDetail.ConfirmTransaction(Trans.Description) == false)
                {
                    if (_unitofWork.SaveChanges() > 0)
                    {
                        msg.Message = string.Format("You have succesfully added {0} to transaction bucket", Trans.Description);
                        msg.ResponseCode = ResponseCode.OK;
                        msg.Result = true;
                    }
                    else
                    {
                        msg.Message = string.Format("Attempt to add {0} to transaction bucket failed", Trans.Description);
                        msg.ResponseCode = ResponseCode.NotOK;
                        msg.Result = false;
                    }
                }
                else
                {
                    msg.Message = string.Format("{0} is already existing in transaction bucket", Trans.Description);
                    msg.ResponseCode = ResponseCode.Null;
                    msg.Result = false;
                }
                return msg;
            }
            catch(Exception ex)
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                return msg;
            }
        }
        public BusinessMessage<List<TransactionDetail>> GetAllTransactions()
        {
            try
            {
               
                BusinessMessage<List<TransactionDetail>> msg = new BusinessMessage<List<TransactionDetail>>();
                var det = _unitofWork.TransactionDetail.GetAll();
                if (det.Count()>0)
                {
                 
                        msg.Message = string.Format("{0} Selected",det.Count());
                        msg.ResponseCode = ResponseCode.OK;
                        msg.Result = det.ToList();
                   
                }
                else
                {
                    msg.Message = string.Format("No Transaction was selected");
                    msg.ResponseCode = ResponseCode.Null;
                    msg.Result = null;
                }
                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<List<TransactionDetail>> msg = new BusinessMessage<List<TransactionDetail>>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = null;
                return msg;
            }
        }

        public BusinessMessage<bool> CreateNewTransactionSplit(TransactionSplit Trans)
        {
            try
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                var oldMaturity = GetTotalSplitMaturityPeriond((int)Trans.TransId);
                var transactionDetail = GetSingleTransactionDetail((int)Trans.TransId);
                int predictiveSum = oldMaturity.Result + (int)Trans.MaturityTime;
                if (predictiveSum < (int)transactionDetail.Result.MaturityTime)
                {
                    _unitofWork.TransactionSplitDetail.Add(Trans);
                    if (_unitofWork.TransactionSplitDetail.ConfirmTransactionSplit(Trans.SplitDescription) == false)
                    {
                        if (_unitofWork.SaveChanges() > 0)
                        {
                            msg.Message = string.Format("You have succesfully added {0} to Transaction Split Bucket", Trans.SplitDescription);
                            msg.ResponseCode = ResponseCode.OK;
                            msg.Result = true;
                        }
                        else
                        {
                            msg.Message = string.Format("Attempt to add {0} to Transaction Slit Bucket failed", Trans.SplitDescription);
                            msg.ResponseCode = ResponseCode.NotOK;
                            msg.Result = false;
                        }
                    }
                    else
                    {
                        msg.Message = string.Format("{0} is already existing in Tansaction Bucket", Trans.SplitDescription);
                        msg.ResponseCode = ResponseCode.Null;
                        msg.Result = false;
                    }
                }
                else {
                    msg.Message = string.Format("The total sum of Maturity Period in split cannot be more that the default maturity period set in transaction detail");
                    msg.ResponseCode = ResponseCode.NotOK;
                    msg.Result = false;
                }
                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                return msg;
            }
        }

        public BusinessMessage<TransactionSplit> GetSingleTransactionSplit(int TransId)
        {
            BusinessMessage<TransactionSplit> msg = new BusinessMessage<TransactionSplit>();
            var trans = _unitofWork.TransactionSplitDetail.GetSingle(TransId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans;
            msg.Message = string.Format("1 item selected");
            return msg;
        }

        public BusinessMessage<List<TransactionSplit>> GetAllTransactionSplit(int transId)
        {
            BusinessMessage<List<TransactionSplit>> msg = new BusinessMessage<List<TransactionSplit>>();
            var trans = _unitofWork.TransactionSplitDetail.GetAllTransactionSplit(transId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans.ToList();
            msg.Message = string.Format("{0} listed", trans.Count());
            return msg;
        }

        public BusinessMessage<List<TransactionSplit>> GetAllTransactionSplit()
        {
            BusinessMessage<List<TransactionSplit>> msg = new BusinessMessage<List<TransactionSplit>>();
            var trans = _unitofWork.TransactionSplitDetail.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans.ToList();
            msg.Message = string.Format("{0} listed", trans.Count());
            return msg;
        }
        public BusinessMessage<bool> EditTransactionSplit(TransactionSplit Trans)
        {
            try
            {
                _unitofWork.TransactionSplitDetail.Edit(Trans, Trans.Id);
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                if (_unitofWork.SaveChanges() > 0)
                {
                    msg.Message = string.Format("You have succesfully edited Transaction Split from Bucket");
                    msg.ResponseCode = ResponseCode.OK;
                    msg.Result = true;
                }
                else
                {
                    msg.Message = string.Format("Attempt to edit {0} to Transaction Split Bucket failed", Trans.SplitDescription);
                    msg.ResponseCode = ResponseCode.NotOK;
                    msg.Result = false;
                }


                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                return msg;
            }
        }

        public BusinessMessage<int> GetTotalSplitMaturityPeriond(int TransId)
        {
            BusinessMessage<int> msg = new BusinessMessage<int>();
            var total = _unitofWork.TransactionSplitDetail.GetAll()
                .Where(c => c.TransId == TransId).Select(d => d.MaturityTime).Sum();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = (int)total;
            msg.Message = "No problems";
            return msg;
        }

        public BusinessMessage<bool> CreateNewPackage(Package Package)
        {
            try
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                    _unitofWork.PackageDetails.Add(Package);
                     if (_unitofWork.SaveChanges() > 0)
                        {
                            msg.Message = string.Format("You have succesfully added {0} to Package Bucket", Package.Description);
                            msg.ResponseCode = ResponseCode.OK;
                            msg.Result = true;
                        }
                        else
                        {
                            msg.Message = string.Format("Attempt to add {0} to Package Bucket failed", Package.Description);
                            msg.ResponseCode = ResponseCode.NotOK;
                            msg.Result = false;
                        }
                   
                return msg;
            }
            catch (Exception ex)
            {
                BusinessMessage<bool> msg = new BusinessMessage<bool>();
                msg.Message = ex.Message;
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                return msg;
            }
        }

        public BusinessMessage<Package> GetSinglePackage(int Id)
        {
            BusinessMessage<Package> msg = new BusinessMessage<Package>();
            var trans = _unitofWork.PackageDetails.GetSingle(Id);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans;
            msg.Message = string.Format("1 item selected");
            return msg;
        }

        public BusinessMessage<List<Package>> GetAllPackages(int transId)
        {
            BusinessMessage<List<Package>> msg = new BusinessMessage<List<Package>>();
            var trans = _unitofWork.PackageDetails.GetAllPackages(transId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans.ToList();
            msg.Message = string.Format("{0} listed", trans.Count());
            return msg;
        }

        public BusinessMessage<List<Package>> GetAllPackages()
        {
            BusinessMessage<List<Package>> msg = new BusinessMessage<List<Package>>();
            var trans = _unitofWork.PackageDetails.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans.ToList();
            msg.Message = string.Format("{0} listed", trans.Count());
            return msg;
        }

        public BusinessMessage<Bank> GetSingleBank(int Id)
        {
            BusinessMessage<Bank> msg = new BusinessMessage<Bank>();
            var trans = _unitofWork.BankDetail.GetSingle(Id);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans;
            msg.Message = string.Format("1 item selected");
            return msg;
        }

        public BusinessMessage<List<Bank>> GetAllBanks()
        {
            BusinessMessage<List<Bank>> msg = new BusinessMessage<List<Bank>>();
            var trans = _unitofWork.BankDetail.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans.ToList();
            msg.Message = string.Format("{0} listed", trans.Count());
            return msg;
        }

        public BusinessMessage<List<UserStatu>> GetAllUserStatus()
        {
            BusinessMessage<List<UserStatu>> msg = new BusinessMessage<List<UserStatu>>();
            var det = _unitofWork.UserStatusDetail.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }

        public BusinessMessage<City> GetSingleCity(int Id)
        {
            BusinessMessage<City> msg = new BusinessMessage<City>();
            var trans = _unitofWork.CityDetails.GetSingle(Id);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans;
            msg.Message = string.Format("1 item selected");
            return msg;
        }

        public BusinessMessage<List<City>> GetAllCities(int stateId)
        {
            BusinessMessage<List<City>> msg = new BusinessMessage<List<City>>();
            var det = _unitofWork.CityDetails.GetAllCities(stateId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }

        public BusinessMessage<List<State>> GetAllStates()
        {
            BusinessMessage<List<State>> msg = new BusinessMessage<List<State>>();
            var det = _unitofWork.StateDetails.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }

        public BusinessMessage<bool> CreateNewUser(User User)
        {
            BusinessMessage<bool> msg = new BusinessMessage<bool>();
            if (_unitofWork.UserDetails.ConfirmUserByEmail(User.EmailAddress) == false)
            {
                if (_unitofWork.UserDetails.ConfirmUserByAccountNo(User.AccountNo) == false)
                {
                    if (_unitofWork.UserDetails.ConfirmUserByPhoneNumber(User.PhoneNo) == false)
                    {
                        _unitofWork.UserDetails.Add(User);
                        if (_unitofWork.SaveChanges() > 0)
                        {
                            msg.ResponseCode = ResponseCode.OK;
                            msg.Result = true;
                            msg.Message = string.Format("Hello, {0} {1}<br/> Your Profile has been Successfully Created", User.Surname, User.Othername);
                        }
                        else
                        {
                            msg.ResponseCode = ResponseCode.NotOK;
                            msg.Result = false;
                            msg.Message = "Techical Error just occured";
                        }
                    }
                    else
                    {
                        msg.ResponseCode = ResponseCode.NotOK;
                        msg.Result = false;
                        msg.Message = "There already exists User with similar Phone Number";
                    }
                }
                else
                {
                    msg.ResponseCode = ResponseCode.NotOK;
                    msg.Result = false;
                    msg.Message = "There already exists User with similar Account Number";
                }
            }
            else
            {
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                msg.Message = "There already exists User with similar Email Address";
            }
            return msg;
        }

        public BusinessMessage<bool> EditUser(User User)
        {
            BusinessMessage<bool> msg = new BusinessMessage<bool>();
            _unitofWork.UserDetails.Edit(User, User.Id);
            if(_unitofWork.SaveChanges()>0)
            {
                msg.ResponseCode = ResponseCode.OK;
                msg.Result = true;
                msg.Message = string.Format("{0} {1} has been successfully updated",User.Surname,User.Othername);
            }
            else
            {
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                msg.Message = string.Format("Unable to process update");
            }
            return msg;
        }

        public BusinessMessage<User> GetUserFromAccountNo(string AccNo)
        {
            BusinessMessage<User> msg = new BusinessMessage<User>();
            var det = _unitofWork.UserDetails.GetUserByAccountNo(AccNo);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det;
            msg.Message = string.Format("1 Item Selected");
            return msg;
        }

        public BusinessMessage<User> GetUserFromEmailAddress(string EmailAddress)
        {
            BusinessMessage<User> msg = new BusinessMessage<User>();
            var det = _unitofWork.UserDetails.GetUserByEmail(EmailAddress);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det;
            msg.Message = string.Format("1 Item Selected");
            return msg;
        }

        public BusinessMessage<List<User>> GetAllUsers()
        {
            BusinessMessage<List<User>> msg = new BusinessMessage<List<User>>();
            var det = _unitofWork.UserDetails.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }

        public BusinessMessage<List<User>> GetAllUsers(int statusId)
        {
            BusinessMessage<List<User>> msg = new BusinessMessage<List<User>>();
            var det = _unitofWork.UserDetails.GetAllUsers(statusId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }
    }
}

