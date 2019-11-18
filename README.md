# Moq.Many
Time-saving extensions to create and setup large number of mocks using Moq framework.

Examples:

Given a class
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

1. You can get every needed mock just typing:
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
