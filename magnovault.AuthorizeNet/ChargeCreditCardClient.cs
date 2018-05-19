using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;


namespace magnovault.Authorize.Net
{
     public class ChargeCreditCardClient
     {
          //private string authorizeNetUrl = "https://apitest.authorize.net/xml/v1/request.api";
          //private string authorizeNetUrl = "https://api.authorize.net/xml/v1/request.api";

          public createTransactionResponse RunCharge(String ApiLoginID, String ApiTransactionKey, creditCardType creditCard, customerAddressType billingAddress, lineItemType[] lineItems, decimal amount)
          {
               ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

               // define the merchant information (authentication / transaction id)
               ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
               {
                    name = ApiLoginID,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = ApiTransactionKey,
               };

               //standard api call to retrieve response
               var paymentType = new paymentType { Item = creditCard };

               var transactionRequest = new transactionRequestType
               {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                    amount = amount,
                    payment = paymentType,
                    billTo = billingAddress,
                    lineItems = lineItems
               };

               var request = new createTransactionRequest { transactionRequest = transactionRequest };

               // instantiate the controller that will call the service
               var controller = new createTransactionController(request);
               controller.Execute();

               // get the response from the service (errors contained if any)
               var response = controller.GetApiResponse();

               // validate response
               if (response != null)
               {
                    return response;
               }
               else
               {
                    return null;
               }
          }
     }  //class
}  //namespace
