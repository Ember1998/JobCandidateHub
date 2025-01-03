# JobCandidateHub

## Requirements
- The software assumes you are using a **Unix-based or Windows operating system** (Linux, macOS or windows) for the best experience. While it may work on Windows, it hasn’t been extensively tested there.
- You have **.NET 8.0** installed for running the backend services.
- A **SQL Server database** is required for persistent storage, and it must be running before the application starts.

## Assumptions
- The codebase follows the **Minimal API** pattern for significant reduction in the usual formalities associated with traditional methods.
- Since, the email of candidate should be used as unique identifier, if email duplicate is found then it'll update the whole record of that candidate associated with that email.
- Time Interval pattern of candidate model should be this pattern **(e.g 1 hours 30 minutes)**
- Email should be valid.
- Automated database migration after running

## List of Improvements
- **Improve error handling:** The current implementation has basic error logging, but it should be extended to handle more edge cases and provide more informative error messages.
- **Add unit tests:** While there is only one integration tests, more comprehensive unit tests can be added.
- **Addition of FrontEnd** Addition of FrontEnd would be much prefereable to 

## Task Breakdown
- **Creating and installing libraries:** 10 mins
- **Model Creation Configuration of Pipelines** 60 mins
- **Configuration of Dbcontext and Migration** 10 mins
- **Logical implementation of endpoint to Add or Update candidate** 60 mins
- **Caching implementation** 10 mins
- **Implementation of Repository pattern** 50 mins
- **Writing Test case** 60 mins
- **Testing whole application** 60 mins
