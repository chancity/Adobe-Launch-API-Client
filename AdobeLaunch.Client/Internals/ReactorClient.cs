using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdobeLaunch.Client.ApiConstants;
using AdobeLaunch.Client.Extensions;
using AdobeLaunch.Client.HelperInterfaces;
using AdobeLaunch.Client.HttpClientMiddleware;
using AdobeLaunch.Client.Models;
using Refit;

namespace AdobeLaunch.Client.Internals
{
    internal class ReactorClient
    {
        internal IReactorClient Client { get; }
        internal HttpClient HttpClient { get; }

        public ReactorClient(AccountOptions accountOptions, ITokenHandler<AccessToken> accessTokenHandler, HttpMessageHandler innerHandler)
        {
            var httpAuthorizationHandler = new HttpAuthorizationHandler(accessTokenHandler, innerHandler);

            HttpClient httpClient = new HttpClient(new HttpExceptionHandler(httpAuthorizationHandler))
            {
                BaseAddress = new Uri(AdobeReactor.Hostname),
            };

            httpClient.SetDefaultRequestHeaders(accountOptions);


            Client = RestService.For<IReactorClient>(httpClient);
            HttpClient = httpClient;
        }
    }

    public interface IReactorClient
    {
        #region Relationships Catch All

        [Get("/{what}/{id}/{relationship}")]
        Task<string> Relationship([AliasAs("what")] string what, [AliasAs("id")] string id,
            [AliasAs("relationship")] string relationship);

        #endregion

        #region Audit Events    

        [Get("/audit_events/{id}")]
        Task<string> AuditEvent([AliasAs("id")] string companyId);

        [Get("/audit_events")]
        Task<string> AuditEvents();

        #endregion

        #region Builds

        [Post("/libraries/{id}/builds")]
        Task<string> CreateBuild([AliasAs("id")] string id);

        [Get("/builds/{id}")]
        Task<string> Build([AliasAs("id")] string id);

        [Get("/builds/{id}/properties")]
        Task<string> Builds([AliasAs("id")] string id);

        #endregion

        #region Callbacks

        [Post("/properties/{id}/callbacks")]
        Task<string> CreateCallback([AliasAs("id")] string id);

        [Delete("/callbacks/{id}")]
        Task<string> DeleteCallback([AliasAs("id")] string id);

        [Get("/callbacks/{id}")]
        Task<string> Callback([AliasAs("id")] string id);

        [Get("/properties/{id}/callbacks")]
        Task<string> Callbacks([AliasAs("id")] string id);

        [Patch("/callbacks/{id}")]
        Task<string> UpdateCallback([AliasAs("id")] string id);

        #endregion

        #region Companies

        [Get("/companies/{id}")]
        Task<string> Company([AliasAs("id")] string id);

        [Get("/companies")]
        Task<string> Companies();

        #endregion

        #region Data Elements

        [Post("/properties/{id}/data_elements")]
        Task<string> CreateDataElement([AliasAs("id")] string id);

        [Delete("/data_elements/{id}")]
        Task<string> DeleteDataElement([AliasAs("id")] string id);

        [Get("/data_elements/{id}")]
        Task<string> DataElement([AliasAs("id")] string id);

        [Get("/properties/{id}/data_elements")]
        Task<string> DataElements([AliasAs("id")] string id);

        [Patch("/data_elements/{id}")]
        Task<string> ReviseDataElement([AliasAs("id")] string id);

        [Patch("/data_elements/{id}")]
        Task<string> UpdateDataElement([AliasAs("id")] string id);

        #endregion

        #region Environments

        [Post("/properties/{id}/environments")]
        Task<string> CreateEnvironment([AliasAs("id")] string id);

        [Delete("/environments/{id}")]
        Task<string> DeleteEnvironment([AliasAs("id")] string id);

        [Get("/environments/{id}")]
        Task<string> Environment([AliasAs("id")] string id);

        [Get("/properties/{id}/environments")]
        Task<string> Environments([AliasAs("id")] string id);

        [Patch("/environments/{id}")]
        Task<string> UpdateEnvironment([AliasAs("id")] string id);

        #endregion

        #region Extension Packages

        [Post("/extension_packages")]
        Task<string> CreateExtensionPackage();

        [Get("/extension_packages/{id}")]
        Task<string> ExtensionPackage([AliasAs("id")] string id);

        [Get("/extension_packages")]
        Task<string> ExtensionPackages([AliasAs("id")] string id);

        [Patch("/extension_packages/{id}")]
        Task<string> ExtensionPackagePrivateRelease([AliasAs("id")] string id);

        [Post("/extension_packages/{id}")]
        Task<string> UpdateExtensionPackage([AliasAs("id")] string id);

        #endregion

        #region Extensions

        [Post("/properties/{id}/extensions")]
        Task<string> CreateExtension([AliasAs("id")] string id);

        [Delete("/extensions/{id}")]
        Task<string> DeleteExtension([AliasAs("id")] string id);

        [Get("/extensions/{id}")]
        Task<string> Extension([AliasAs("id")] string id);

        [Get("/properties/{id}/extensions")]
        Task<string> Extensions([AliasAs("id")] string id);

        [Patch("/extensions/{id}")]
        Task<string> ReviseExtension([AliasAs("id")] string id);

        #endregion

        #region Hosts

        [Post("/properties/{id}/hosts")]
        Task<string> CreateHost([AliasAs("id")] string id);

        [Delete("/hosts/{id}")]
        Task<string> DeleteHost([AliasAs("id")] string id);

        [Get("/hosts/{id}")]
        Task<string> Host([AliasAs("id")] string id);

        [Get("/properties/{id}/hosts")]
        Task<string> Hosts([AliasAs("id")] string id);

        [Patch("/hosts/{id}")]
        Task<string> UpdateHost([AliasAs("id")] string id);

        #endregion

        #region Libraries

        [Post("/properties/{id}/libraries")]
        Task<string> CreateLibrary([AliasAs("id")] string id);

        [Get("/libraries/{id}")]
        Task<string> Library([AliasAs("id")] string id);

        [Get("/properties/{id}/libraries")]
        Task<string> Libraries([AliasAs("id")] string id);

        [Post("/libraries/{id}/builds")]
        Task<string> PublishLibrary([AliasAs("id")] string id);

        [Patch("/libraries/{id}")]
        Task<string> TransitionLibrary([AliasAs("id")] string id);

        [Patch("/libraries/{id}")]
        Task<string> UpdateLibrary([AliasAs("id")] string id);

        #endregion

        #region Profiles

        [Get("/profile")]
        Task<string> Profile();

        #endregion

        #region Properties

        [Post("/companies/{id}/properties")]
        Task<string> CreateProperty([AliasAs("id")] string id);

        [Delete("/properties/{id}")]
        Task<string> DeleteProperty([AliasAs("id")] string id);

        [Get("/properties/{id}")]
        Task<string> Property([AliasAs("id")] string id);

        [Get("/companies/{id}/properties")]
        Task<string> Properties([AliasAs("id")] string id);

        [Patch("/properties/{id}")]
        Task<string> UpdateProperty([AliasAs("id")] string id);

        #endregion

        #region Rule Components

        [Post("/properties/{id}/rule_components")]
        Task<string> CreateRuleComponent([AliasAs("id")] string id);

        [Delete("/rule_components/{id}")]
        Task<string> DeleteRuleComponent([AliasAs("id")] string id);

        [Get("/rule_components/{id}")]
        Task<string> RuleComponent([AliasAs("id")] string id);

        [Get("/rules/{id}/rule_components")]
        Task<string> RuleComponents([AliasAs("id")] string id);

        [Patch("/rule_components/{id}")]
        Task<string> UpdateRuleComponent([AliasAs("id")] string id);

        #endregion

        #region Rules

        [Post("/properties/{id}/rules")]
        Task<string> CreateRule([AliasAs("id")] string id);

        [Delete("/rules/{id}")]
        Task<string> DeleteRule([AliasAs("id")] string id);

        [Get("/rules/{id}")]
        Task<string> Rule([AliasAs("id")] string id);

        [Get("/properties/{id}/rules")]
        Task<string> Rules([AliasAs("id")] string id);

        [Patch("/rules/{id}")]
        Task<string> ReviseRule([AliasAs("id")] string id);

        [Patch("/rules/{id}")]
        Task<string> UpdateRule([AliasAs("id")] string id);

        #endregion
    }
}