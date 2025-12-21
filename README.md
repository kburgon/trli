# trli

A simple Trello client for the CLI.

Why would I do such a thing?  See my post on [my Substack](https://kevinburgon.substack.com/p/trli-a-week-long-hackathon).

## Overview

Trli is a simple CLI client that lists boards, lists, and cards, and performs basic card manipulation.  It is by no means complete, and may or may not be added to over time.

## Installation

### Requirements

- .NET 10.0 SDK (yes, the SDK.  I currently have no published libraries avaliable that can be used without compiling locally)

### Instructions

Clone the repository and run the shell script pertaining to your OS and architecture above.  If no installation script has been created for your architecture/OS, open one of the script files to see the command that publishes the app and places it.

Once the app is published, the compiled binary needs to be marked as executable and placed in a directory that is included in the OS's PATH structure.

## Usage

The app can do the following things:

- List the boards that a member belongs to
- List the lists belonging to a given boards
- List the cards in a board by list ID or card ID
- Update the contents of a card by card ID
- Create a card and add it to a list

For more information, add `trli --help`.
