using CleanApp.Domain.Constants;
using CleanApp.Domain.Enums;

namespace CleanApp.Domain.Entities
{
    public class BookingEntity
    {
        public BookingEntity(Guid id, Guid userId, DateTime fromDate, DateTime toDate, short guests, bool breakfastIncluded, RoomType type, string? indications, BookingStatus status)
        {
			this.id = id;
			this.userId = userId;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.guests = guests;
            this.breakfastIncluded = breakfastIncluded;
            this.roomType = type;
            this.indications = indications;
            this.status = status;
        }

        public BookingEntity(Guid userId, DateTime fromDate, DateTime toDate, short guests, bool breakfastIncluded, RoomType type, string? indications, BookingStatus status) 
		{
			id = Guid.NewGuid();

            this.userId = userId;
            this.fromDate = fromDate;
			this.toDate = toDate;
			this.guests = guests;
			this.breakfastIncluded = breakfastIncluded;
			this.roomType = type;
			this.indications = indications;
			this.status = status;
		}

		private readonly Guid id;

		public Guid Id
		{
			get { return id; }
		}

		private Guid userId;

		public Guid UserId
		{
			get { return userId; }
		}


		private DateTime fromDate;

		public DateTime FromDate
		{
			get { return fromDate; }
			set { fromDate = value; }
		}

		private DateTime toDate;

		public DateTime ToDate
		{
			get { return toDate; }
			set { toDate = value; }
		}

		private short guests;

		public short Guests
		{
			get { return guests; }
			set { guests = value; }
		}

		private bool breakfastIncluded;

		public bool BreakfastIncluded
		{
			get { return breakfastIncluded; }
			set { breakfastIncluded = value; }
		}

		private RoomType roomType;

		public RoomType RoomType
		{
			get { return roomType; }
			set { roomType = value; }
		}

		private string? indications;

		public string? Indications
		{
			get { return indications; }
			set { indications = value; }
		}

		private BookingStatus status;

		public BookingStatus Status
		{
			get { return status; }
			set { status = value; }
		}
	}
}
