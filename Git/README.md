# GitHub Change Commit Author

Set Notepad as default git text editor
git config core.editor notepad

git rebase -i -p <commit>

Notepad opens with a list of commits. Overwrite "pick" with "edit" for the commit you want to change the Author

git commit --amend --author "Author Name &lt;email&gt;"

git rebase --continue

git push -f origin master
