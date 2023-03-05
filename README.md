<h1 style="text-align:center">SonarAssist Manual</h1>

This is still a demo, there may be problems in some of its functions.

-----

# 1. General

## 1.1 Functions

SonarAssist is a command-line tool for Java project evaluation, which integrated SonarQube for code analysis. It can batch analyze multiple projects, and use pre-trained data to generate comprehensive results for each of them.

## 1.2 Java Project

A Java project is considered to be a separate folder, which directly contains a `src` subfolder for all `.java` files. The name of the root folder is regarded as the default name of the project.

```
project_folder/
 |- src/
 |   |- *.java
 |   \- package/
 |       |- ...
 \- whatever/
```

A bunch of Java projects can be organized in any form, just as long as they are separated.

```
projects/
 |- projectA/
 \- groupA/
     |- projectB/
     |- projectC/
     \- groupB/
         |- projectD
```

## 1.3 Command Line Syntax

It uses verb options to execute different commands.

```bash
sonar-assist <command> [options...] [<arguments>...]
```

-----

# 2. Commands

## 2.1 config

> Set global SonarAssist configurations. Global configuration file `config.coin` is stored in `config/` folder with the executable file.

```bash
$ sonar-assist config [-s <server>] [-t <token>] [-p <profile>]
```

- **-s, --server** (optional): Set target SonarQube server. It must contain protocol (`http://` or `https://`).
- **-t, --token** (optional): Set SonarQube access token. Token must have administrative permission.
- **-p, --profile** (optional): Set target quality profile. If this is set, new projects will be associated to the specific quality profile. This profile must exist on your SonarQube server.

SonarQube server and token must be set before any actions that related to remote server.

## 2.2 scan

> Scan Java project(s).

```bash
$ sonar-assist scan [-r] [-f] [-t] -d <root_dir> [-n project_name]
```

- **-r, --recursive** (optional): Scan Java projects recursively. If this is set, scan action will take `root_dir` as a collection of Java projects, and analyze all of them. Otherwise, it will take `root_dir` as the root folder of only one project, and it must contain `src` folder directly.
- **-f, --force** (optional): Force update all projects. If this is **not** set, scan action will skip scanned projects to avoid redundant works. Or, it will rescan the projects anyway.
- **-t, --temp** (optional): Whether make compiled `.class` files temporary or not. If this is set, the compiled `.class` files will be deleted after scan. If not, `.class` files will be kept in `sonar-assist/output` folder in corresponding project.
- **-d, --directory** (required): Specific target directory.
- **-n, --name** (optional): Only valid if `-r` is **not** set, or it will be ignored. It will set the name of the project specifically instead of using root folder name.

If project has not been scanned yet, it will first be initialized like this:

```bash
project_dir/
 |- src/
 |- sonar-assist/            # newly created
 |   \- sonar-assist.coin    # project configuration
 \- sonar-project.properties # SonarQube project properties file
```

Whether a project is initialized is determined by `sonar-assist.coin`.

Scan action will not update analysis result. To get the latest result, use `sonar-assist update`.

> SonarQube server will take some extra time to update database after SonarScanner completes, get analysis result immediately may not get latest issues. So, issues should better be updated later manually.

## 2.3 update

> Update analysis result. This will update analysis result for all specified projects.

```bash
$ sonar-assist update [-r] -d <root_dir> [-o output_dir]
```

- **-r, --recursive** (optional): The same as that of `scan`.
- **-d, --directory** (required): Specific target directory.
- **-o, --output** (optional): Specific output directory for final analysis report. If set, all analysis reports will be put to this directory.

Update action will generate `issues.json` in `sonar-assist/`, which is the raw data from SonarQube server. Then, it will use pre-trained data to generate analysis report to a new folder `{project_name}-Report/` under `sonar-assist/` or the specified directory. The result is presented as html file.

> WARNING: Please ensure that there are no duplicated project names.

## 2.4 clean

> Clean SonarAssist scan results. It will delete `sonar-assist/` folder in specified projects.

```bash
$ sonar-assist clean [-r] -d <root_dir>
```

- **-r, --recursive** (optional): The same as that of `scan`.
- **-d, directory** (required): Specific target directory.

