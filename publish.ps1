# Publish script for creating a GitHub repo and pushing the current project
# Requires: git and GitHub CLI (`gh`) installed and authenticated.

param(
    [string]$RepoName = "PhotonRedirect",
    [string]$Description = "BepInEx plugin to override Photon settings",
    [string]$Visibility = "public" # or 'private'
)

# Initialize git repo if needed
if (-not (Test-Path .git)) {
    git init
    git add .
    git commit -m "Initial commit"
}

# Create GitHub repo using gh CLI (preferred)
if (Get-Command gh -ErrorAction SilentlyContinue) {
    gh repo create $RepoName --$Visibility --description "$Description" --source=. --remote=origin --push
    Exit $LASTEXITCODE
}

Write-Host "gh CLI not found. To publish manually, run these commands:" -ForegroundColor Yellow
Write-Host "git remote add origin https://github.com/<your-username>/$RepoName.git"
Write-Host "git branch -M main"
Write-Host "git push -u origin main"
