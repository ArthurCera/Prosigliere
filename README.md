For Database creationg before running the code

Add-Migration -p Model -Context DBContext initialmigration
Update-Database -p Model -context DBContext

Run the project CodingChallenge


			--- About the project ---

This a RESTful API project for managing the blog contents and comments.

The project currently contains -->
Api for the creation of a blog post; POST /api/posts

Api for commenting on a blog post; POST /api/posts{id}/comments

Api for retrieving all the blog posts; GET /api/posts

Api for retrieving a blog post with the commentaries; GET /api/posts{id}


Subsequent steps -->
Add a new field to flag a comment with spoilers ??;

Add the user guid on both the comments and the blog posts;

Add the functionallity to edit and remove both the blog posts and commentaries;

Add authentication(login with e-mail/pass and jwt) and user management (??);

Add database migrations on runtime;

Add pagination and filtering on the endpoint's;

Caching to improve performance for the frequently accessed data.

Add more validation for the data sent to the server as needed.

Implement better error logging (save errors on the database for the critical system areas);

Review the functions and identify potential optmizations;

Implement file upload and download for the posts and commentaries.

Implement tags and category systems for the posts.

Tools for admins to moderate both the posts and comments (delete, issues warnings, etc...);

E-mail, SMS notifications for post/comment threads;

Api rate limiting ?? limit user access based on subscriptions;

Optimization of unit tests;

Feature to block other users posts and comments from appearing on the users side;

User view history (latest seen posts, comments);
Monitoring tools for both api usage and performance.
			--------------------------------
