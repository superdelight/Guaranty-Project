using PonziBussinessLogic.Interface;
using PonziRepostiory;
using PonziRepostiory.Implementation;
using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PonziBussinessLogic.Implementation
{
    public class PonziAdminLogin: IPonziAdminLogic
    {
        IUnitofWork _unitofWork = new UnitofWork();


        public BusinessMessage<bool> CreateNewPackage(MPackage Package)
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
        public BusinessMessage<MPackage> GetSinglePackage(int Id)
        {
            BusinessMessage<MPackage> msg = new BusinessMessage<MPackage>();
            var trans = _unitofWork.PackageDetails.GetSingle(Id);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = trans;
            msg.Message = string.Format("1 item selected");
            return msg;
        }
        public BusinessMessage<List<MPackage>> GetAllPackages()
        {
            BusinessMessage<List<MPackage>> msg = new BusinessMessage<List<MPackage>>();
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
        public List<ValidationStatus> GetAllValidationStatus()
        {
            return _unitofWork.ValidationStatusContext.GetValidationAllStatus().ToList();
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

        public BusinessMessage<bool> CreateNewRegistrant(Registrant Registrant)
        {
            BusinessMessage<bool> msg = new BusinessMessage<bool>();
            if (_unitofWork.UserDetails.ConfirmUserByEmail(Registrant.EmailAddress) == false)
            {
                if (_unitofWork.UserDetails.ConfirmUserByAccountNo(Registrant.AccountNo) == false)
                {
                    if (_unitofWork.UserDetails.ConfirmUserByPhoneNumber(Registrant.PhoneNo) == false)
                    {

                        GetAllValidationStatus();
                        _unitofWork.EmailProxyContext.GetActiveProxySetting();
                     
                        _unitofWork.UserDetails.Add(Registrant);
                        if (_unitofWork.SaveChanges() > 0)
                        {

                            var usr = _unitofWork.UserDetails.GetUserByEmail(Registrant.EmailAddress);
                            EmailValidation emVal = new EmailValidation();
                            emVal.EmailCode = GetEmailCode().Result;
                            emVal.GeneratedTime = DateTime.Now;
                            emVal.IsActive = true;
                            emVal.UserId = usr.Id;
                            emVal.Status =Convert.ToInt32(ValidationStatusEnum.Email_Code_Generated);


                            PhoneValidation phoneVal = new PhoneValidation();
                            phoneVal.GeneratedTime = DateTime.Now;
                            phoneVal.PhoneCode = GetPhoneCode().Result;
                            phoneVal.Remarks = null;
                            phoneVal.UserId = usr.Id;
                            phoneVal.Status= Convert.ToInt32(ValidationStatusEnum.Phone_Code_Generated);
                            phoneVal.IsActive = true;


                            CreateNewEmailVerification(emVal);
                            CreateNewPhoneVerificaton(phoneVal);




                            msg.ResponseCode = ResponseCode.OK;
                            msg.Result = true;
                            msg.Message = string.Format("Hello, {0} {1}<br/> Your Profile has been Successfully Created", Registrant.Surname, Registrant.Othername);
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
                        msg.Message = "There already exists Registrant with similar Phone Number";
                    }
                }
                else
                {
                    msg.ResponseCode = ResponseCode.NotOK;
                    msg.Result = false;
                    msg.Message = "There already exists Registrant with similar Account Number";
                }
            }
            else
            {
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                msg.Message = "There already exists Registrant with similar Email Address";
            }
            return msg;
        }
        public BusinessMessage<bool> EditRegistrant(Registrant Registrant)
        {
            BusinessMessage<bool> msg = new BusinessMessage<bool>();
            _unitofWork.UserDetails.Edit(Registrant, Registrant.Id);
            if(_unitofWork.SaveChanges()>0)
            {
                msg.ResponseCode = ResponseCode.OK;
                msg.Result = true;
                msg.Message = string.Format("{0} {1} has been successfully updated",Registrant.Surname,Registrant.Othername);
            }
            else
            {
                msg.ResponseCode = ResponseCode.NotOK;
                msg.Result = false;
                msg.Message = string.Format("Unable to process update");
            }
            return msg;
        }
        public BusinessMessage<Registrant> GetRegistrantFromAccountNo(string AccNo)
        {
            BusinessMessage<Registrant> msg = new BusinessMessage<Registrant>();
            var det = _unitofWork.UserDetails.GetUserByAccountNo(AccNo);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det;
            msg.Message = string.Format("1 Item Selected");
            return msg;
        }
        public BusinessMessage<Registrant> GetRegistrantFromEmailAddress(string EmailAddress)
        {
            BusinessMessage<Registrant> msg = new BusinessMessage<Registrant>();
            var det = _unitofWork.UserDetails.GetUserByEmail(EmailAddress);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det;
            msg.Message = string.Format("1 Item Selected");
            return msg;
        }

        public BusinessMessage<List<Registrant>> GetAllRegistrants()
        {
            BusinessMessage<List<Registrant>> msg = new BusinessMessage<List<Registrant>>();
            var det = _unitofWork.UserDetails.GetAll();
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }
        public BusinessMessage<List<Registrant>> GetAllRegistrants(int statusId)
        {
            BusinessMessage<List<Registrant>> msg = new BusinessMessage<List<Registrant>>();
            var det = _unitofWork.UserDetails.GetAllUsers(statusId);
            msg.ResponseCode = ResponseCode.OK;
            msg.Result = det.ToList();
            msg.Message = string.Format("{0} listed", det.Count());
            return msg;
        }
        public BusinessMessage<MPackage> GetDefaultPackage()
        {
            BusinessMessage<MPackage> msg = new BusinessMessage<MPackage>();
            var det = _unitofWork.PackageDetails.GetSingle(1);
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
            if (_unitofWork.SaveChanges() > 0)
            {
             
                string DomainName = HttpContext.Current.Request.Url.Host;
                int port = HttpContext.Current.Request.Url.Port;
                string msg = string.Format("<html><body><h2>Account Activation Message</h2><p>Hello {0},</p><p>Your Login ID is <b>{1}</b><br/>Password is: <b>{2}</b> </p><p>Thank you for Signing up for your helper's Account.<br/><a href=http://{3}:{4}/email-activation?code={5}>Kindly click here to make your account fully functional... </a> </p></body></html>",  emailVerification.User.Surname, emailVerification.User.Username, emailVerification.User.Password, DomainName,port,emailVerification.EmailCode);
                response.ResponseCode = ResponseCode.OK;
                response.Message = "You have successfully saved created Phone Verification";
                response.Result = true;
                _unitofWork.EmailProxyContext.SendEmail(true, msg, "Activation Mail", emailVerification.User.EmailAddress);

                response.ResponseCode = ResponseCode.OK;
                response.Message = "";
                response.Result = true;

            }
            return response;
        }
        public BusinessMessage<EmailValidation> GetRegistrantEmailVerification(string email)
        {
            throw new NotImplementedException();
        }
        private EmailValidation GetUserEmailCode(string email)
        {
            return _unitofWork.EmailValidationContext.GetRecentEmailCode(email);
        }
        private PhoneValidation GetUserPhoneCode(string email)
        {
            return _unitofWork.PhoneValidationContext.GetRecentPhoneCode(email);
        }
        public bool VerifyEmail(string email)
        {
            //var emailVerification=_unitofWork.EmailValidationContext\
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> CreateNewPhoneVerificaton(PhoneValidation phoneValidation)
        {
            BusinessMessage<bool> response = new PonziBussinessLogic.BusinessMessage<bool>();
            _unitofWork.PhoneValidationContext.Add(phoneValidation);
            if (_unitofWork.SaveChanges() > 0)
            {
                response.ResponseCode = ResponseCode.OK;
                response.Message = "You have successfully saved created Email Verification";
                response.Result = true;
                _unitofWork.SMSProxySetting.SendSMS(phoneValidation.User.PhoneNo, string.Format("Account Activation Code is {0}. It is valid for 24 hours", phoneValidation.PhoneCode), "GTH");
                response.ResponseCode = ResponseCode.OK;
                response.Message = "";
                response.Result = true;

            }
            return response;
           
        }

        public BusinessMessage<PhoneValidation> GetRegistrantPhoneVerification(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyPhone(string email)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<bool> VerifyEmail(string email, string code)
        {
            BusinessMessage<bool> response = new PonziBussinessLogic.BusinessMessage<bool>();
            if(_unitofWork.EmailValidationContext.ConfirmEmailCode(email, code))
            {
                var emailVal = _unitofWork.EmailValidationContext.GetRecentEmailCode(email);
                if(emailVal.EmailCode==code)
                {
                    emailVal.Status = Convert.ToInt32(ValidationStatusEnum.Email_Verified);
                    _unitofWork.SaveChanges();
                    response.ResponseCode = ResponseCode.OK;
                    response.Message = string.Format("Your Email Activation is successful...");
                    response.Result = true;
                }
                else
                {
                    response.ResponseCode = ResponseCode.NotOK;
                    response.Message = string.Format("The Code you entered has expired...");
                    response.Result = true;
                }
            }
            else
            {
                response.ResponseCode = ResponseCode.NotOK;
                response.Message = string.Format("Unable to verify Email Activation Code...");
                response.Result = true;
            }
            return response;
        }

        public BusinessMessage<bool> VerifyPhone(string email, string code)
        {
            BusinessMessage<bool> response = new PonziBussinessLogic.BusinessMessage<bool>();
            if (_unitofWork.PhoneValidationContext.ConfirmPhoneCode(email, code))
            {
                var emailVal = _unitofWork.PhoneValidationContext.GetRecentPhoneCode(email);
                if (emailVal.PhoneCode == code)
                {
                    emailVal.Status = Convert.ToInt32(ValidationStatusEnum.Phone_Verified);
                    _unitofWork.SaveChanges();
                    response.ResponseCode = ResponseCode.OK;
                    response.Message = string.Format("Your Phone Activation is successful...");
                    response.Result = true;
                }
                else
                {
                    response.ResponseCode = ResponseCode.NotOK;
                    response.Message = string.Format("The Code you entered has expired...");
                    response.Result = true;
                }
            }
            else
            {
                response.ResponseCode = ResponseCode.NotOK;
                response.Message = string.Format("Unable to verify Phone Activation Code...");
                response.Result = true;
            }
            return response;
        }

        public bool ActivateUserEmail(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public bool ActivateUserPhoneNumber(string userId, string code)
        {
            throw new NotImplementedException();
        }

        public BusinessMessage<Registrant> GetRegistrant(string loginId)
        {
            BusinessMessage<Registrant> response = new PonziBussinessLogic.BusinessMessage<Registrant>();
            var reply = _unitofWork.UserDetails.GetUser(loginId);
            if (reply != null)
            {
                response.ResponseCode = ResponseCode.OK;
                response.Message = "Response OK";
                response.Result = reply;
            }
            else
            {
                response.ResponseCode = ResponseCode.OK;
                response.Message = "Response OK";
                response.Result = reply;
            }
            return response;
        }
    }
}

