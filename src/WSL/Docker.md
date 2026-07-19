# Docker with WSL2

````bash
docker --version                    # Verify Docker CLI is available (via Docker Desktop WSL integration)
docker ps                           # List running containers
````

## Setup

Enable WSL integration in:

**Docker Desktop → Settings → Resources → WSL Integration**

Then toggle on the distro(s) you want Docker available in.

## Notes

* Docker Desktop uses WSL2 as its backend on Windows — no separate Docker install needed inside the distro
* Alternatively, install Docker Engine natively inside WSL (via systemd, see `09-systemd.md`) for a Docker Desktop-free setup
