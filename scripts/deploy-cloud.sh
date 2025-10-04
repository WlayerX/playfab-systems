#!/usr/bin/env bash
set -euo pipefail
# Deploy CloudScript files to PlayFab using PlayFab Admin API (requires TITLE_ID and DEV_SECRET)
# WARNING: store secrets securely (CI secrets, not in repo)

TITLE_ID="\${PLAYFAB_TITLE_ID:-YOUR_TITLE_ID}"
DEV_SECRET="\${PLAYFAB_DEV_SECRET:-YOUR_DEV_SECRET}"

if [ "\$TITLE_ID" = "YOUR_TITLE_ID" ]; then
  echo "Set PLAYFAB_TITLE_ID and PLAYFAB_DEV_SECRET env vars before running."
  exit 1
fi

SRC_DIR="\$(cd "\$(dirname "\${BASH_SOURCE[0]}")/../server-functions/src" && pwd)"
echo "Packaging CloudScript from \$SRC_DIR..."

# Create upload JSON: list all handlers from files
TMP_JSON="\$(mktemp)"
jq -n '{}' > "\$TMP_JSON"

# Very simple packaging: each file becomes an element; for production use PlayFab CLI or SDK
FILES=\$(find "\$SRC_DIR" -name 'cloudscript-*.js' -print)
HANDLERS_JSON=""
COUNT=0
for f in \$FILES; do
  NAME=\$(basename "\$f")
  echo "Including \$NAME"
  CODE=\$(sed '1,2000p' "\$f" | sed -e 's/"/\\"/g' -e ':a;N;$!ba;s/\\n/\\\\n/g')
  # Not robust: for illustration only. Prefer official PlayFab CLI/SDK.
  HANDLERS_JSON="\${HANDLERS_JSON}\n  { \"Name\": \"\${NAME}\", \"Code\": \"\${CODE}\" },"
  COUNT=\$((COUNT+1))
done

if [ \$COUNT -eq 0 ]; then
  echo "No cloudscript files found. Nothing to deploy."
  exit 1
fi

# Construct payload
PAYLOAD="{ \"TitleId\": \"\$TITLE_ID\", \"Files\": [${HANDLERS_JSON%,}] }"

echo "Payload prepared. (Not sending in this demo script)"

# For production, use:
# curl -X POST "https://TITLE_ID.playfabapi.com/Admin/SetCloudScript" \
#   -H "X-SecretKey: \$DEV_SECRET" \
#   -H "Content-Type: application/json" \
#   -d "\$PAYLOAD"

echo "Deploy script finished. Replace the echo with a real call using PlayFab Admin API or PlayFab CLI."
