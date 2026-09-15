# Notes: blank player field on edit

## What happened

Opening a player's card in edit mode sometimes showed blank Name / Number / Position
fields instead of the player's data, with no error anywhere in the UI.

## Investigation

- Added logging in [`player-edit.component.ts`](frontend/src/app/player-edit/player-edit.component.ts)
  to log the player id being requested and the API response received. The console showed
  the correct id going out on every request, and the API returning `null` for the
  affected players - so the frontend was sending the right thing and wasn't the cause.
- Added a debug log in [`PlayersController.cs`](backend/RosterManager.Api/Controllers/PlayersController.cs)
  at the point where `GetById` searches the roster table. It showed the lookup was only
  ever searching the first 1,000 of 180,000+ total players, regardless of where the
  requested id actually lived in the table.

## Root cause

`GET /api/players/{id}` fetched a fixed-size page of the player table (`.Take(1000)`)
and then searched *within that page* for the requested id. Any player whose row fell
outside the first 1,000 - which, on a table that grows over time, is most players added
after the table passed 1,000 rows - would never be found. The endpoint returned `200 OK`
with an empty body instead of an error, so the UI had nothing to distinguish "this
player doesn't exist" from "this player exists but the API didn't look far enough."

## Fix shipped

Raised the hardcoded page size so the lookup covers the current table
(`backend/RosterManager.Api/Controllers/PlayersController.cs`). This unblocks the
immediate issue and was small enough to ship through normal code review.

## Long-term fix (not yet done)

Raising the constant only buys time - the table keeps growing, so this same bug comes
back the next time it passes the new limit. The real fix is a dedicated single-record
endpoint (e.g. `GET /api/players/{id}` backed by a direct lookup - a database query
keyed on `Id`, or a dictionary over the in-memory store) instead of paging a chunk of
the table and filtering it client-side. That's correct at any table size and doesn't
depend on a constant someone has to remember to keep raised.

Tracked as a `TODO` in `PlayersController.cs`.
