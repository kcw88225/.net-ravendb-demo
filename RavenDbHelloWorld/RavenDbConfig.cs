using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System;
using System.Reflection;
public static class RavenDbConfig
{
    private static IDocumentStore _store;

    public static IDocumentStore Store
    {
        get
        {
            if (_store == null)
                throw new InvalidOperationException("IDocumentStore has not been initialized.");

            return _store;
        }
    }

    public static IDocumentStore Initialize()
    {
        _store = new EmbeddableDocumentStore
        {
            ConnectionStringName = "RavenDB"
        };

        _store.Conventions.IdentityPartsSeparator = "-";
        _store.Initialize();
        IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);

        return _store;
    }
}