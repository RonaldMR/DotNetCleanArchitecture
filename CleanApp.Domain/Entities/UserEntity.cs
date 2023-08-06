namespace CleanApp.Domain.Entities
{
    public class UserEntity
    {
		public UserEntity(string firstName, string lastName, string emailAddress, string password)
		{
			if(string.IsNullOrWhiteSpace(firstName))
			{
				throw new ArgumentNullException(nameof(firstName));
			}

			if(string.IsNullOrWhiteSpace(lastName))
			{
				throw new ArgumentNullException(nameof(lastName));
			}

			if(string.IsNullOrWhiteSpace(emailAddress))
			{
				throw new ArgumentNullException(nameof(@emailAddress));
			}

			if(string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			id = Guid.NewGuid();

			this.firstName = firstName;
			this.lastName = lastName;
			this.emailAddress = emailAddress;
			this.password = password;

			creationDate = DateTime.Now;
		}

        public UserEntity(Guid id, string firstName, string lastName, string emailAddress, string password, DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException(nameof(@emailAddress));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

			this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.emailAddress = emailAddress;
            this.password = password;
			this.creationDate = creationDate;
        }

        private readonly Guid id;

		public Guid Id
		{
			get { return id; }
		}


		private string firstName;

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		private string lastName;

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		private string emailAddress;

		public string EmailAddress
		{
			get { return emailAddress; }
			set { emailAddress = value; }
		}

		private string password;

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		private readonly DateTime creationDate;

		public DateTime CreationDate
		{
			get { return creationDate; }
		}

	}
}
