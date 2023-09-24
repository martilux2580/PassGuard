<div id="PassGuardLogo" align="center">
    <br />
    <img src="./PassGuard/Images/Logo.png#gh-light-mode-only" alt="PassGuardLogo" width="200"/>
    <img src="./PassGuard/Images/Logoblancblanc.png#gh-dark-mode-only" alt="PassGuardLogo" width="200"/>
    <h1><b>PassGuard</b></h1>
</div>

To the point, **PassGuard is a modern-looking, fully offline and open source password manager** whose job is to securely store and provide to the user an active management of their passwords.

*Read this in other languages: [English](README.md), [Spanish](README.es.md).*

## Why does PassGuard exist? PassGuard Origins
----------------------------------------------

<p>The idea of creating a password manager came firstly from the <b>desire to create a personal project</b> using the knowledge obtained in the first semesters of the Computer Engineering degree.<br> 
The first thing I did was to <b>look for (IT-related) processes in my life</b> that I could optimise. Among the first processes I thought of was the idea of creating a password manager. 
Within my close circle only one person uses a professional password manager, the rest of the people saved their passwords in a plain text file, wrote them by hand on a piece of paper or used the same password with small variations.<br>
In my case, <b>although the current password managers on the market have good reviews</b>, some have suffered security breaches (apparently without consequences for the users), and <b>the fact of saving all your valuable passwords in one place made me wary of these services</b>. That's why <b>I decided to create my own password manager :)</b><br>
I learned about the algorithms and security protocols that are used to store this data securely, and the result of this research and implementation is PassGuard.</p>

## List of Features
-------------------

**PassGuard** has a variety of characteristics to suit different types of users, from beginners to advanced users. The list of those features is as follows:

- Version 1.1:
    - Added ability to **mark passwords as important**, and to sort them at the top.
    - Improved **outline color manager**, with possibility to **import and export colour settings**.
    - Added ability to **generate and download statistics about saved passwords**.
    - Added option to **run Passguard at Windows startup**, or **keep it running in the background** when closing it.

- Version 1.0:
    - **Create, load and save** PassGuard Vaults in the .encrypted format (compatible with PassGuard)
    - **Organised and secure password storage** in your PassGuard Vault
    - Strong **password generator**
        - Possibility of **generating passwords that have not been previously cracked**.
    - **Export data** from your PassGuard Vault **to a PDF**
    - **Create backups and Automated Backups** of a selected PassGuard Vault (app must be running to do so)
    - **Light/Dark Theme and Outline Colour Customization**, saving your preferences for future executions

Create and use strong passwords, save and manage them and **stop worrying about the management and security of your passwords with PassGuard**.

## Ideas for future versions
-------------------------------
Surely there are ways to leave me ideas for future versions. Some ideas I am contemplating are the following:
- Window size not fixed and maximizable.
- Improve visual aspect on screens with different resolutions and sizes, since for 24" monitors or 15" laptops, both with 1080p resolution, the application works fine, however with other configurations there may be glitches.
- Add more statistics to the password part, and evaluate it for the outline colors part.

## Installation and Dependencies
--------------------------------

**PassGuard runs correctly on 64-bit distributions of Windows**, however, depending on the version of Passguard, different dependencies are required.

Dependencies for each version are the following:
- Version **1.1**:
    - Windows OS (Windows 10 or 11, haven´t checked on the other ones), 64 bits.
    - .NET7.0 distribution for 64 bits.

- Version **1.0**:
    - Windows OS (Windows 10, haven´t checked on the other ones), 64 bits.
    - Both 32 and 64 bit distributions of .NET Framework 4.6.

For **basic users**, in the **Releases section of this repository** you can find the **stable versions of the application**. Inside each Release you will find the `.zip` file with the `.exe` installers for each supported OS. To **install the application** on your system just **download** the `.zip` file corresponding to your operating system, **unzip** it and **run** the `setup.exe` file.<br>

For **advanced users** who want to compile and modify the code, the `.sln` file for Visual Studio (preferably 2019 or newer editions, depending on Passguard version) is available in the main branch of this repository. </p>

## Disclaimer
-------------

This software is **open source** and there is **no warranty of any type** associated with it. **Use at your own risk**.

Designed by **martilux2580**
