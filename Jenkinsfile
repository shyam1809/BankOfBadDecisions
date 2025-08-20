pipeline {
  agent any

  environment {
    DOCKERHUB_REPO = "shyam2004/bankofbaddecisions"
    IMAGE_TAG = "latest"
  }

  stages {
    stage('Checkout') {
      steps { checkout scm }
    }
    stage('Build (.NET in container)') {
      steps {
        bat 'docker run --rm -v "%cd%":/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet restore'
        bat 'docker run --rm -v "%cd%":/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet build -c Release'
      }
    }
    stage('Test') {
      steps {
        bat 'echo "No tests defined"'
      }
    }
    stage('Docker Build') {
      steps {
        bat 'docker build -t %shyam2004/bankofbaddecisions%:%latest% .'
      }
    }
    stage('Docker Push') {
      steps {
        withCredentials([usernamePassword(credentialsId: 'dockerhub-cred', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
          bat 'echo %DOCKER_PASS% | docker login -u %DOCKER_USER% --password-stdin'
          bat 'docker push %DOCKERHUB_REPO%:%IMAGE_TAG%'
        }
      }
    }
  }
  post {
    success { echo 'Build & push successful!' }
    failure { echo 'Build failed.' }
  }
}
