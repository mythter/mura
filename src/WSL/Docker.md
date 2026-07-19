# How to Install Docker Engine on WSL

This guide shows how to install **Docker Engine inside WSL2** on Windows (no Docker Desktop required).

## Prerequisites

* **Windows 10/11** with **WSL2** enabled and a Linux distro installed (Ubuntu recommended)

Notes:

* Commands below are intended to run in a **WSL terminal**.
* On WSL2, published ports are typically reachable from Windows at `localhost`.

## (Optional) Run WSL commands from Windows PowerShell

If you prefer to stay in a single Windows terminal, you can run the WSL commands via `wsl.exe`.

Pick your distro name (examples: `Ubuntu`, `Ubuntu-22.04`). List distros:

````powershell
wsl -l -v
````

Run bash command in WSL:

````powershell
wsl -d Ubuntu -- bash -lc "docker version"
````

Tip: use `bash -lc` so your normal shell profile/path applies.

## Install Docker Engine in WSL

From WSL (Ubuntu):

````bash
sudo apt update
sudo apt install -y ca-certificates curl gnupg

sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
sudo chmod a+r /etc/apt/keyrings/docker.gpg

echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo \"$VERSION_CODENAME\") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

sudo apt update
sudo apt install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
````

Allow non-root usage (log out/in after this):

````bash
sudo usermod -aG docker $USER
````

Start Docker:

If your WSL distro has systemd enabled:

````bash
sudo systemctl start docker
````

Otherwise: start the daemon in a separate terminal:

````bash
sudo dockerd
````

(Optional) Enable Docker autostart with WSL:

````bash
sudo systemctl enable docker
````

Verify:

````bash
docker version
docker compose version
````

## Use Docker in WSL from Windows

If you want to use Docker running in WSL from Windows applications (e.g., running tests with TestContainers or managing containers with command line), you need to expose the Docker daemon over TCP.

### 1) Configure Docker daemon to listen on TCP

In WSL, create or edit `/etc/docker/daemon.json`:

````bash
sudo mkdir -p /etc/docker
sudo nano /etc/docker/daemon.json
````

Add the following content:

````json
{
  "hosts": ["tcp://127.0.0.1:2375", "unix:///var/run/docker.sock"]
}
````

Save and exit (`Ctrl+O`, `Enter`, `Ctrl+X`).

### 2) Override systemd service configuration

If your WSL distro uses systemd, create an override file:

````bash
sudo mkdir -p /etc/systemd/system/docker.service.d
sudo nano /etc/systemd/system/docker.service.d/override.conf
````

Add the following content:

````ini
[Service]
ExecStart=
ExecStart=/usr/bin/dockerd
````

Save and exit.

### 3) Reload systemd and restart Docker

````bash
sudo systemctl daemon-reload
sudo systemctl restart docker
````

Verify Docker is listening on TCP:

````bash
docker -H tcp://127.0.0.1:2375 version
````

### 4) Configure Windows to use Docker in WSL

In Windows PowerShell, set the `DOCKER_HOST` environment variable:

````powershell
setx DOCKER_HOST tcp://127.0.0.1:2375
````

**Important:** After setting the environment variable, you must restart:

* All open PowerShell/Command Prompt windows
* Visual Studio, Rider, or any IDE you're using
* Any application that needs to use Docker

The environment variable change only affects new processes, not existing ones.

### 5) Install Docker CLI on Windows

To use Docker commands from Windows, you need to install the Docker CLI (command-line interface).

**Using winget:**

````powershell
winget install Docker.DockerCLI
````

**Alternative - Manual download:**

Download Docker CLI from [Docker releases](https://download.docker.com/win/static/stable/x86_64/) and extract `docker.exe` to a folder in your PATH (e.g., `C:\Program Files\Docker\`).

After installation, restart PowerShell to ensure the `docker` command is available.

### 6) Test from Windows

From Windows PowerShell or Command Prompt:

````powershell
docker version
docker ps
````

You should see the Docker daemon running in WSL.

### Security Note

**Warning:** Exposing Docker daemon on TCP without TLS is insecure for production environments. This setup is only recommended for local development on `127.0.0.1` (localhost).

For production or remote access, configure TLS authentication. See [Generate certificates for docker engine](https://github.com/lbruun-net/docker-free?tab=readme-ov-file#step-2---generate-certificates-for-docker-engine) for details.
