# GitHub Change Commit Author

Set Notepad as default git text editor
```
git config core.editor notepad
```
Start rewriting history like changing author or delete a commit.

Important: commit should be one BEFORE the one to change!
```
git rebase -i -p <commit> ### Only if <commit> is not latest commit
```

Notepad opens with a list of commits. Overwrite "pick" with "edit" for the commit you want to change the author.

```
git commit --amend --author "Author Name <email>"
git rebase --continue ### Only if <commit> is not latest commit
git push -f
```

Get current hash

```
git rev-parse --short HEAD
```
