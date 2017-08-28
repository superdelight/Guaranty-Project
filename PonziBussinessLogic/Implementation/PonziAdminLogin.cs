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
                            var usr = _unitofWork.UserDetails.GetUserByEmail(User.EmailAddress);
                            EmailValidation emVal = new EmailValidation();
                            emVal.EmailCode = GetEmailCode().Result;
                            emVal.GeneratedTime = DateTime.Now;
                            emVal.IsActive = true;
                            emVal.UserId = usr.Id;
                            emVal.Status =Convert.ToInt32(ValidationStatusEnum.Email_Code_Generated);

                            CreateNewEmailVerification(emVal);



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

        public BusinessMessage<Package> GetDefaultPackage()
        {
            BusinessMessage<Package> msg = new BusinessMessage<Package>();
            var det = _unitofWork.PackageDetails.GetDefaultPackage();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det;
            msg.Message = string.Format("One Item Selected...");
            return msg;
        }
        public BusinessMessage<string> GetEmailCode()
        {
            BusinessMessage<string> response = new PonziBussinessLogic.BusinessMessage<string>();
            try
            {
                Random rand = new Random();
                int newVal = rand.Next(1, 9);
                int secVal = rand.Next(10, 98);
                response.Result = string.Format("EM{0:D3}{1}{2:D2}{3:D2}{4:D2}{5}", DateTime.Now.DayOfYear, newVal, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, secVal);
            }
            catch
            {
                response.ResponseCode = ResponseCode.NotOK;
                response.Message = "Unable to generate Code";
            }
            return response;
        }
        public BusinessMessage<string> GetPhoneCode()
        {
            BusinessMessage<string> response = new PonziBussinessLogic.BusinessMessage<string>();
            try
            {
                Random rand = new Random();
                int newVal = rand.Next(1, 9);
                int secVal = rand.Next(10, 98);
                response.Result = string.Format("{0:D3}{1}{2:D2}{3:D2}{4:D2}{5}", DateTime.Now.DayOfYear, newVal, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, secVal);
            }
            catch
            {
                response.ResponseCode = ResponseCode.NotOK;
                response.Message = "Unable to generate Code";
            }
            return response;
        }
        public BusinessMessage<bool> CreateNewEmailVerification(EmailValidation emailVerification)
        {
            BusinessMessage<bool> response = new PonziBussinessLogic.BusinessMessage<bool>();
            _unitofWork.EmailValidationContext.Add(emailVerification);
            if(_unitofWork.SaveChanges()>0)
            {
                response.ResponseCode = ResponseCode.OK;
                response.Message = "You have successfully saved created Email Verification";
                response.Result = true;
                _unitofWork.SMSProxySetting.SendSMS(emailVerification.User.PhoneNo, string.Format("Activation Code {0}", GetPhoneCode()), "GTH");
                _unitofWork.EmailProxyContext.SendEmail(true, "OK", "ACTIVATION CODE", emailVerification.User.EmailAddress);
                response.ResponseCode = ResponseCode.OK;
                response.Message = "";
                response.Result = true;
                
            }
            return response;   
        }

        public BusinessMessage<EmailValidation> GetUserEmailVerification(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyEmail(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<PhoneValidation> CreateNewPhoneVerificaton(PhoneValidation phoneValidation)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<PhoneValidation> GetUserPhoneVerification(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyPhone(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyEmail(string email, string code)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyPhone(string email, string code)
        {
            throw new NotImplementedException();
        }
    }
}

