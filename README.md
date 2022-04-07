# Many.Mocks
Time-saving extensions to create and setup large number of mocks using Moq framework.

👉Download releases at https://www.nuget.org/packages/Many.Mocks/

## **Content**
* How to get the mocks from any method or constructor?
* How to get a mock from any public method?
* How to get a mock from every property type of a class/interface?
* How to instantiate a class injecting a bag of mocks?
* How to add custom mocks when you try to instante a class?
* How to get and setup a mock from a bag?

## **How to use**
If you have a class with several properly-injected dependencies...
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
  
### **How to get the mocks from any method or constructor?**
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
### **How to get a mock from any public method?**
```
var mocks = typeof(UserManager).GetMocksFrom("uniqueMethodName");
```

If you have more than one method with different signatures:

```
var methodSignatureOrderedTypes = new List<Type> { typeof(Interface1), typeof(Interface2) };
var mocks = typeof(UserManager).GetMocksFrom("OverridedMethodName", methodSignatureOrderedTypes);
```

### **How to get a mock from every property type of a class/interface?**
```
var mocks = typeof(UserManager).GetMocksFromProperties();
```

### **How to instantiate a class injecting a bag of mocks?**
There are several ways depending on wether the default mocks are valid for you or not. But the easiest way is:
```
var mocks = typeof(UserManager)
                .GetMocksFromConstructors(); //Get mocks from constructor

var result = mocks.TryInstantiate(out UserManager result); //Get the instance
```

### **How to add custom mocks when you try to instante a class?**
```
var mocks = typeof(UserManager)
                .GetMocksFromConstructors()
                .Select(p => p.Details); //Get mocks from constructor

var customMockToReplace = new Mock<IServiceProvider>();
var result = mocks.TryInstantiate(new List<Mock>() {customMockToReplace}, out UserManager result); //Get the instance replacing your custom mock
```


### **How to get and setup a mock from a bag?**
```
var mocks = typeof(UserManager)
                .GetMocksFromConstructors(); //Get mocks from constructor

var m = mocks.First<Interface1>();

m.Setup(p => bla, bla, bla);
```

If you have a more complex bag of mocks with different mocks for the same type:
```
var mocks = typeof(UserManager)
                .GetMocksFromConstructors(); //Get mocks from constructor

if (!mocks.TryFind(out IList<Inteface1> listOfInteface1Mocks))
    throw new Exception(); //Mock not found!!!


```