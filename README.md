# Roster Manager

A small ASP.NET Core + Angular app for editing a sports team's player roster.

This is a **recreated, illustrative case study**, built for my portfolio. It walks
through a debugging process I went through on a real internship project, but every
line of code here is original and written from scratch for this repo - there is no
proprietary code, data, schema, or naming from that project anywhere in this codebase.
The bug, the domain (a fictional sports roster tool), and the fix are all invented for
this demo; they are not a claim that this exact code shipped anywhere.

The commit history is the point of the repo: each commit is a deliberate step in a
debugging story, from the initial bug through investigation to the fix and a note on
the proper long-term solution. See [NOTES.md](NOTES.md) for the write-up, and
[`screenshots/`](screenshots) for the walkthrough.

## Stack

- **Backend:** ASP.NET Core Web API (`backend/RosterManager.Api`), in-memory data only
- **Frontend:** Angular (`frontend`)

## Running it locally

```bash
# API (http://localhost:5218)
cd backend/RosterManager.Api
dotnet run

# Frontend (http://localhost:4200)
cd frontend
npm install
npm start
```
