pipeline {
  agent any

  environment {
    DOCKERHUB_REPO = "your-dockerhub-username/bankofbaddecisions"
    IMAGE_TAG = "latest"
  }

  stages {
    stage('Checkout') {
      steps { checkout scm }
    }
    stage('Build (.NET in container)') {
      steps {
        sh 'docker run --rm -v "$PWD":/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet restore'
        sh 'docker run --rm -v "$PWD":/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet build -c Release'
      }
    }
    stage('Test') {
      steps {
        // Placeholder - add unit tests when available
        sh 'echo "No tests defined"'
      }
    }
    stage('Docker Build') {
      steps {
        sh 'docker build -t $DOCKERHUB_REPO:$IMAGE_TAG .'
      }
    }
    stage('Docker Push') {
      steps {
        withCredentials([usernamePassword(credentialsId: 'dockerhub-cred', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
          sh 'echo $DOCKER_PASS | docker login -u $DOCKER_USER --password-stdin'
          sh 'docker push $DOCKERHUB_REPO:$IMAGE_TAG'
        }
      }
    }
  }
  post {
    success { echo 'Build & push successful!' }
    failure { echo 'Build failed.' }
  }
}
