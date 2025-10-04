#!/usr/bin/env bash
set -euo pipefail
git init
git add .
git commit -m "chore: initial enterprise playfab skeleton"
git branch -M main
echo "Git initialized and initial commit created."
