﻿// See https://aka.ms/new-console-template for more information

using MKKDotNetCore.ConsoleApp;

var adoDotNetExample = new AdoDotNetExample();

// adoDotNetExample.ReadAll();

// adoDotNetExample.ReadOne(10);

// adoDotNetExample.DeleteOne(10);

// adoDotNetExample.CreateOne(title: "NextJs", content: "NextJs is a framework over react", author: "MKK");

adoDotNetExample.UpdateOne(14, title: "NextJs", content: "NextJs is a framework over react", author: "MKK");

Console.ReadLine();