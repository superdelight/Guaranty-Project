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
    public class PonziPledgeLogic : IPonziPledgeLogic
    {
        IUnitofWork _unitofWork = new UnitofWork();

        public BusinessMessage<bool> CreateUserPackage(UserPackage package)
        {
            BusinessMessage<bool> response = new BusinessMessage<bool>();
            var lastPackage = _unitofWork.UserPackageContext.GetUserLastPackage((int)package.UserId);
            var pack = _unitofWork.PackageDetails.GetSingle((int)package.PackId);
            if(lastPackage==null)
            {
                _unitofWork.UserPackageContext.Add(package);
                if(_unitofWork.SaveChanges()>0)
                {
                  string msg= string.Format("<h1>Congratulations</h1>You have now pledged the sum of =N={0:N2}. You will be matched to provide Help in the next {1} to {2} days. Precisely {3}", package.Amount, pack.MinimumPHDays, pack.MaximumPHDays, package.CompletionDate);
                    response.Message = msg;
                    response.ResponseCode = ResponseCode.OK;
                    response.Result = true;
                }
                else
                {
                    string msg = "Unable to proceed";
                    response.Message = msg;
                    response.ResponseCode = ResponseCode.NotOK;
                    response.Result = false;
                }
            }
            else
            {
                if(lastPackage.PackStatus==Convert.ToInt32(PackageStatusEnum.Closed))
                {
                    string msg = string.Format("<h1>Congratulations</h1>You have now pledged the sum of =N={0:N2}. You will be matched to provide Help in the next {1} to {2} days. Precisely {3}", package.Amount, pack.MinimumPHDays, pack.MaximumPHDays, package.CompletionDate);
                    response.Message = msg;
                    response.ResponseCode = ResponseCode.OK;
                    response.Result = true;
                }
                else
                {
                    string msg = "You Still have an active Transaction";
                    response.Message = msg;
                    response.ResponseCode = ResponseCode.NotOK;
                    response.Result = false;
                }
            }
            return response;
        }

        public BusinessMessage<string> GetPackageMessage(string username)
        {
          
                BusinessMessage<string> response = new PonziBussinessLogic.BusinessMessage<string>();
            try
            {
                var registant = _unitofWork.UserDetails.GetUser(username);
                if (registant != null)
                {
                    if (registant.NobleStatus == Convert.ToInt32(RoleStatusEnum.Outbound_User))
                    {
                        //var userPackage = _unitofWork.UserPackageContext.GetCurrentUserPackage(registant.Id, Convert.ToInt32(PackageStatusEnum.Active));
                        var userPackage = _unitofWork.UserPackageContext.GetUserLastPackage(registant.Id);
                        if (userPackage != null)
                        {
                            var package = _unitofWork.PackageDetails.GetSingle((int)userPackage.PackId);
                            //Calculate the Remaining Time...
                            var pledgedSince = DateTime.Now.Subtract(userPackage.DateStarted.Value);
                            var remainingTime =userPackage.CompletionDate.Value.Subtract(DateTime.Now);
                            if (userPackage.PackStatus == Convert.ToInt32(PackageStatusEnum.Closed))
                            {
                                string msg = string.Format("<h1>Hello {0}</h1><p>Your Last Transaction Ended on {1}</p> <p>You can now proceed to Create another transaction but note that you will not be allowed to do transaction that is less than =N= {2:N2}.</p>", registant.Surname, userPackage.CompletionDate.Value.ToLongDateString(), userPackage.Amount);
                                response.Message = msg;
                                response.ResponseCode = ResponseCode.Closed;
                                response.Result = msg;
                            }
                            else if (userPackage.PackStatus == Convert.ToInt32(PackageStatusEnum.Matched_For_PH))
                            {

                            }
                            else if (userPackage.PackStatus == Convert.ToInt32(PackageStatusEnum.Matched_For_GH))
                            {

                            }
                            else if (userPackage.PackStatus == Convert.ToInt32(PackageStatusEnum.Active))
                            {

                                if (remainingTime.Days > 1)
                                {
                                    string msg = string.Format("<h1>Hello {0}</h1><p>You initiated a transaction on {1}, this transaction is still active and our System has placed you on Queue.<br/>You will be matched to <b>Provide Help</b> in the next <b>{2} days</b>.</p> <p>Please prepare yourself for Payment of =N={3:N2}.</p>", registant.Surname, userPackage.DateStarted.Value.ToLongDateString(), remainingTime.Days, userPackage.Amount);
                                    response.Message = msg;
                                    response.ResponseCode = ResponseCode.Active;
                                    response.Result = msg;
                                }
                                else if (remainingTime.Days == 1)
                                {
                                    string msg = string.Format("<h1>Hello {0}</h1><p>You initiated a transaction on {1}, this transaction is still active and our System has placed you on Queue.<br/>You will be matched to <b>Provide Help</b> in the next <b>{2} day</b>.</p> <p>Please prepare yourself for Payment of =N={3:N2}.</p>", registant.Surname, userPackage.DateStarted.Value.ToLongDateString(), remainingTime.Days, userPackage.Amount);
                                    response.Message = msg;
                                    response.ResponseCode = ResponseCode.Active;
                                    response.Result = msg;
                                }
                                else
                                {
                                    Random rand = new Random();
                                    int roundFigure = rand.Next(1, 3);
                                    if (roundFigure == 1)
                                    {
                                        string msg = string.Format("<h1>Congratulations {0}</h1><p> Your PH  Matching has been successfully completed by the system. Please take note of the following details.<br/>You will be paying the sum of {1} <br/>RECIPIENT NAME ==> AKINYODE IDOWU SAMUEL<br/> <br/> BANK NAME ==> UBA<br/>ACCOUNT NUMBER: 011134444<br/>PHONE NUMBER ==> 09015685555.", registant.Surname, userPackage.Amount);
                                        response.Message = msg;
                                        response.ResponseCode = ResponseCode.Matched_For_PH;
                                        response.Result = msg;
                                    }
                                    else if (roundFigure == 2)
                                    {
                                        string msg = string.Format("<h1>Congratulations {0}</h1><p> Your PH  Matching has been successfully completed by the system. Please take note of the following details.<br/>You will be paying the sum of {1} <br/>RECIPIENT NAME ==> BANKOLE RICHARD <br/> <br/> BANK NAME ==> GTB<br/>ACCOUNT NUMBER: 0122444444<br/>PHONE NUMBER ==> 08015685555.", registant.Surname, userPackage.Amount);
                                        response.Message = msg;
                                        response.ResponseCode = ResponseCode.Matched_For_PH;
                                        response.Result = msg;
                                    }

                                    else
                                    {
                                        string msg = string.Format("<h1>Congratulations {0}</h1><p> Your PH  Matching has been successfully completed by the system. Please take note of the following details.<br/>You will be paying the sum of {1} <br/>RECIPIENT NAME ==> BOLA AHMED TINUBU<br/> <br/> BANK NAME ==> ECOBANK<br/>ACCOUNT NUMBER: 0111344477<br/>PHONE NUMBER ==> 07015685555.", registant.Surname, userPackage.Amount);
                                        response.Message = msg;
                                        response.ResponseCode = ResponseCode.Matched_For_PH;
                                        response.Result = msg;
                                    }
                                }
                            }
                            else
                            {
                                response.Message = string.Format("<h1>TECHNICAL ERROR.....</h1>");
                                response.ResponseCode = ResponseCode.Error;
                                response.Result = string.Format("<h1>TECHNICAL ERROR....</h1>");
                            }


                        }
                        else
                        {
                            var package = _unitofWork.PackageDetails.GetDefaultPackage();
                            string msg = string.Format("<h1>Hello {0}</h1><p>You are welcome to Guaranty Trust Helpers Platform. You can pledge as Lower as =N={1:N2} and as higher as =N={2:N2}, you will get a {3}% increase of your Pledged Amount between {4} to {5} days./p>", registant.Surname, package.MinimumAmount, package.MaximumAmount, package.PercentageIncrease, package.MinimumGHDays, package.MaximumGHDays);
                            response.Message = msg;
                            response.ResponseCode = ResponseCode.Null;
                            response.Result = msg;
                        }
                    }
                    else
                    {
                        response.Message = string.Format("<h1>TECHNICAL ERROR</h1>");
                        response.ResponseCode = ResponseCode.Error;
                    }
                }
                else
                {
                    response.Message = string.Format("<h1>TECHNICAL ERROR</h1>");
                    response.ResponseCode = ResponseCode.Error;
                    response.Result = string.Format("<h1>TECHNICAL ERROR</h1>");
                }
            }
            catch
            {

            }
            return response;
            
            
        }

        public UserPackage GetUserActivePackage(string userID)
        {
            var reg = _unitofWork.UserDetails.GetUser(userID);
            return _unitofWork.UserPackageContext.GetCurrentUserPackage(reg.Id,Convert.ToInt32(PackageStatusEnum.Active));
        }
    }
}

