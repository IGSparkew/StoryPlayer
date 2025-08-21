class ServiceLoader {

    public static Dictionary<string, object> services = new Dictionary<string, object>();

    public static void RegisterService<T>(string name, T service) {
        if (services.ContainsKey(name)) {
            throw new Exception($"Service '{name}' is already registered.");
        }

        if (service == null) {
            throw new ArgumentNullException(nameof(service), "Service cannot be null.");
        }

       services.Add(name, service);
    }

    public static T GetService<T>(string name) {
        if (!services.ContainsKey(name)) {
            throw new Exception($"Service '{name}' is not registered.");
        }

        return (T)services[name];
    }}