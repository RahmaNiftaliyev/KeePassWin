# CHANGELOG

## 2.7.0 - 6/26/2018
### Fixed
- Search resets when no text is in the search box

### Updated
- Updated supporting libraries and bumped minimum Windows version

## 2.6.1 - 10/5/2017
### Fixed
- Password generator crashed in Store build

## 2.6.0 - 10/3/2017
### Added
- Password generator is available
- KeePass files (kdbx and kdb) can now be launched to start the application

### Fixed
- Search does not flash now
- Add support for kdb files
- Images were sometimes being mixed up in rendering

## 2.5.3 - 3/12/2017
### Added
- Show keyboard shortcut on context menus

### Fixed
- Keyboard shortcuts were not being honored on entry items
- Search box did not clear on escape
- Switched the font when showing password to Consolas to help distinguish characters such as 'I','l' and '1'
- Fixed clipboard cleared notification to show what happened

## 2.5.2 - 1/27/2017
### Fixed
- Entry icons were taking entire column and making it hard to see anything else

## 2.5.1 - 1/23/2017
### Fixed
- Adding an item to the clipboard would sometimes result in a crash

## 2.5.0 - 1/20/2017
### Added
- Clipboard is cleared after a customizable timeout
- Strings are now localizable. Please see [tracking issue](https://github.com/twsouthwick/KeePassWin/issues/44) for details
- Key handling is customizable per instance

### Fixed
- Fields were not updating as expected
- Adding items in search mode put the item in an ambiguous location
- Navigation on mobile required multiple taps
- View in mobile was truncated 

## 2.4.0 - 12/21/2016
### Added
- Enable creation and renaming of groups
- Add keyboard shortcuts (see [List](KeyboardShortcuts.md) for more detail)

### Fixed
- Save button would sometimes not show. It is now available at all times (even when no changes are present)
- Added scrolling to entry details page for small screens.
- Fixed URL button failure to update when changed
- Fixed crash when supplied key is incorrect
- Fixed tab navigation to be more intuitive

## 2.3.0 - December 3, 2016
### Added
- Streamlined searching that doesn't require leaving the database view
- Clear unlockded databases on suspend
- Initial settings control including a way to toggle logging and clear on suspend

### Fixed
- Entry notes were not being surfaced. Now they show up in the entry view and in search results
- Cleaned up UI, including master/details view for entries and nice breadcrumb navigation for groups
- Fixed some issues with navigation when failure to unlock database

## 2.2.2 - November 15, 2016
### Added
- Open a KBDX 2.0 file with a password and/or keyfile
- Add and edit entries
- Supports simple commands such as copy to clipboard, open URLs
- Universal app for desktop and mobile
