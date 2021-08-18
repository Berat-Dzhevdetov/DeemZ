# DeemZ

![image](https://user-images.githubusercontent.com/56674380/129486206-6f0a40c7-fe25-46cd-8b84-933e5e3d2532.png)

This project is made with ASP.NET Core 5. The design is taken from [SoftUni](https://softuni.bg/) for educational purposes!

ASP.NET Core web application for online programming learing where you can take exams after the course and receive points.

## 🛠 Built with:
- ASP.NET Core MVC
- MS SQL Server
- Cloudinary
- Font-awesome
- Bootstrap

## Permissions:
Permission | Guest | Logged User | Admin
-- | ---- | ---- | ---
Index page | ✅ | ✅ | ✅
Privacy page | ✅ | ✅ | ✅
Forum | ✅ | ✅ | ✅
View Course Details | ✅ | ✅ | ✅
Add report to resource | ❌ | ✅ | ✅
Sign up for the course by paying | ❌ | ✅ | ❌
View Course Resources | ❌ | ✅ (only if the user has paid for the course)| ✅
Download Course Resources | ❌ | ✅ (only if the user has paid for the course)| ✅
Admin Dashboard  | ❌ | ❌ | ✅
Add Course  | ❌ | ❌ | ✅
Edit Course  | ❌ | ❌ | ✅
Delte Course  | ❌ | ❌ | ✅
Add Lecture to Course  | ❌ | ❌ | ✅
Add Exam to Course  | ❌ | ❌ | ✅
Edit Exam  | ❌ | ❌ | ✅
Delete Exam  | ❌ | ❌ | ✅
Edit Lecture  | ❌ | ❌ | ✅
Delete Lecture  | ❌ | ❌ | ✅
Upload Resource to Lecture  | ❌ | ❌ | ✅
Delete Resource | ❌ | ❌ | ✅
Edit User | ❌ | ❌ | ✅
Sign Up User to Course (basically for adding lecturer to the course) | ❌ | ❌ | ✅
Remove User From Course | ❌ | ❌ | ✅
Delete report | ❌ | ❌ | ✅

## Pages:

### Public Pages:

**Home Page**

This is the landing page of the application, from here you can read infromation about the company.
![image](https://user-images.githubusercontent.com/56674380/129486952-c0732410-b630-4eab-98b8-f5e451f72315.png)

**Forum Topics**

In this page, all written topics are displayed, here you can get brief information about the topic. You can also search topic by title using the search bar on the top of the page.
![image](https://user-images.githubusercontent.com/56674380/129487234-70a9fea8-2a4b-462a-985b-04934093adc2.png)



### Pages for Logged Users:
**Posting a Topic**

From this page, you can create a new topic. After choosing an appropriate title and description you can click the button Create in the bottom of the form.
![image](https://user-images.githubusercontent.com/56674380/129487333-1704af2b-e5b5-4921-b5c9-8ab6dbab1362.png)

