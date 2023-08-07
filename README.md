
# User Story

As a User, I want to be able to create/edit bookings for accommodation. 

When creating the booking, I should enter the following information:

* Checking date (Required)
* Checkout date (Required)
* Number of Guests (Required)
* Breakfast Included Yes/No (Optional/ No default)
* Room Type (Single/Double/Presidential/Suite) (Required)
* Indications (Optional)

Consider the following business rules for the booking

* The Checking date should be less than the Checkout date.
* The number of Guests should not surpass the capacity of each room type (e.g. a single room only allows one guest)

When editing the booking, I should only update the status. Consider the following status update flow:

* From Open to Confirmed / Cancelled.
* From Confirmed to Closed.

Cancelled / Closed are the last status a booking have.