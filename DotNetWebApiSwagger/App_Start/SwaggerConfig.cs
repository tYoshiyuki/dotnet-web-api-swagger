using DotNetWebApiSwagger;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DotNetWebApiSwagger
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "DotNetWebApiSwagger");
                        c.DocumentFilter<FakesDocumentFilter>();
                    })
                .EnableSwaggerUi();
        }

        private class FakesDocumentFilter : IDocumentFilter
        {
            private const string Path = "Fake";

            public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
            {
                swaggerDoc.paths.Add($"/api/{Path}/{{id}}", CreateGetPathItem());
                swaggerDoc.paths.Add($"/api/{Path}", CreatePostPathItem());
            }

            private PathItem CreateGetPathItem()
            {
                var x = new PathItem
                {
                    // getメソッド
                    get = new Operation
                    {
                        tags = new[] { Path },
                        operationId = Path + "_Get",
                        consumes = null,
                        produces = new[] { "application/json", "text/json", "application/xml", "text/xml" },
                        parameters = new List<Parameter>
                        {
                            new Parameter
                            {
                                name = "id",
                                @in = "path",
                                required = true,
                                type = "integer",
                                format = "int32"
                            }
                        },
                        responses = new Dictionary<string, Response>
                        {
                            {
                                "200",
                                new Response
                                {
                                    description = "OK",
                                    schema = new Schema {type = "string"}
                                }

                            }
                        }
                    }
                };
                return x;
            }

            private PathItem CreatePostPathItem()
            {
                var x = new PathItem
                {
                    // putメソッド
                    put = new Operation
                    {
                        tags = new[] { Path },
                        operationId = Path + "_Put",
                        consumes = null,
                        produces = new[] { "application/json", "text/json", "application/xml", "text/xml" },
                        parameters = new List<Parameter>
                        {
                            new Parameter
                            {
                                name = "InputModel",
                                @in = "body",
                                required = true,
                                schema = new Schema()
                            }
                        },
                        responses = new Dictionary<string, Response>
                        {
                            {
                                "200",
                                new Response
                                {
                                    description = "OK",
                                    schema = new Schema {type = "string"}
                                }

                            }
                        }
                    }
                };
                return x;
            }

        }
    }
}
