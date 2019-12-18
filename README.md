# Crawler
Simple Crawler and Indexer and Search Engine Web Application
![](https://github.com/pourmand1376/Crawler/blob/master/Demo/Demo1.gif)
![](https://github.com/pourmand1376/Crawler/blob/master/Demo/Demo2.gif)


![release](https://img.shields.io/github/license/pourmand1376/Crawler.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/0sgkbd7r0lf9cf2r?svg=true)](https://ci.appveyor.com/project/pourmand1376/crawler)


## Nuget Restore
Just open the project and right click the solution and choose **nuget** package restore. Wait till package restore completes. 

## Configuration
1. Build and run the first project called Crawler. It uses its seed and downlaods the sites recursively (_Breath First Search_) and stores it in ***Data.Db*** and ***Crawler.Db*** file. Whenever you feel the gathered data is enough, simply close the program.  

2. Build and run the second project called Indexer. You should copy ***Crawler.Db*** file from previous section here. After opening the program, It starts indexing the downloaded data and generates three files ***Sites.Db***, ***TitleIndex.Db***, and ***BodyIndex.Db***.

3. Copy files generated from previous section to **App_Data** folder. 

Enjoy. 
