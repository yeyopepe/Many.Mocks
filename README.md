# Many.Mocks
Time-saving extensions to create and setup large number of mocks using Moq framework.

## **How to use**

You have a class with lots of mocks...
```
public class UserManager : UserManager<User>
    {
        /// <summary>
        /// Ctor. 1
        /// </summary>
        public UserManager(IUserStore<User> store, 
                            IOptions<IdentityOptions> optionsAccessor)
        {
        }
        /// <summary>
        /// Ctor. 2
        /// </summary>
        public UserManager(IUserStore<User> store, 
                            IOptions<IdentityOptions> optionsAccessor,
                            IPasswordHasher<User> passwordHasher, 
                            IEnumerable<IUserValidator<User>> userValidators, 
                            IEnumerable<IPasswordValidator<User>> passwordValidators, 
                            ILookupNormalizer keyNormalizer, 
                            IdentityErrorDescriber errors, 
                            IServiceProvider services,
                            ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            
        }

        public void Method(IUserPasswordStore<User> store) { ... }
        public void Method(IUserStore<TUser> store) { ... }
    }
```
  
### **How to generate a bag of mocks from any method or constructor?**
1. You can get a bag of nedeed mocks just typing:
```
var mocks = typeof(UserManager).GetMocksFromConstructors(); //11 mocks: 2 from ctor. 1 and 9 from ctor. 2
```

2. And also you can get only mocks from one specific constructor:
```
var mocks = typeof(UserManager).GetMocksFromConstructors(new List<Type>{ typeof(IUserStore<User>), typeof(IOptions<IdentityOptions>) }); //2 mocks from ctor. 1
```

3. Same works for any method:
```
var mocks = typeof(UserManager).GetMocksFrom("method", new List<Type>{ typeof(IUserPasswordStore<User>) }); //1 mock
```

If a class is not proxiable and no mock can be created you can check it in the details:
```
var noMockCouldBeGeneratedForTheseClasses = mocks.Mocks.Where(p => !p.Generated); //Get the errors
var ex = noMockCouldBeGeneratedForTheseClasses.Error; //The thrown exception during mock generation
```

### **How to generate a mock for every property type of a class/interface?**
Just ask for them :)
```
var mocks = typeof(UserManager).GetMocksFromProperties();
```

### **How to instantiate a class injecting a bag of mocks?**
```
var mocks = typeof(UserManager).GetMocksFromConstructors().Mocks.Select(p => p.Details); //Get mocks from constructor

var result = mocks.UseToTryInstantiate(out UserManager result); //Get the instance
```
