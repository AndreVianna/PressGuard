
# Remote Pressure Monitoring System for Patient Beds

## Executive Summary
The objective is to engineer a Remote Pressure Monitoring System (RPMS) to monitor patient positioning on bed mattresses in hospital or retirement home settings, aiming to mitigate the risk of bed sores. The architecture comprises sensor packages, a local computer dashboard, and a cloud-based reporting interface, with real-time data acquisition, local and remote monitoring, and historical data analysis.

---

## 1. System Architecture

### 1.1 Sensor Packages
Each sensor package embodies a Raspberry Pi unit and eight DKARDU FSR402 pressure sensors.

**Hardware Components**:
- Raspberry Pi (with display)
- DKARDU FSR402 Pressure Sensors x8
- Analog-to-Digital Converters (ADCs)

**Software Components**:
- Language: C++
- Libraries: WiringPi, MCP3008 library for interfacing with ADCs

**Functionalities**:
- Real-time pressure data acquisition
- Local data processing for average pressure and continuous pressure detection above a configurable threshold
- Local display UI showing real-time and processed data

### 1.2 Local Computer Dashboard
The local computer acts as an intermediary for data aggregation and forwarding.

**Hardware Components**:
- Standard PC

**Software Components**:
- Language: C#
- Framework: Blazor WebAssembly
- Libraries: SignalR for real-time data communication

**Functionalities**:
- Real-time data reception from multiple sensor packages
- Data forwarding to the AWS cloud
- Web-based dashboard displaying real-time data with configurable alerts

### 1.3 AWS Cloud-based Data Storage and Reporting Interface
AWS infrastructure will manage data storage, processing, and web-based reporting.

**AWS Services**:
- Amazon DynamoDB: NoSQL database for data storage
- AWS Lambda: Serverless data processing
- AWS Kinesis: Real-time data streaming
- AWS Amplify: Hosting the Blazor WebAssembly applications
- AWS Cognito: User authentication and authorization
- AWS IoT Core: Secure, bi-directional communication
- AWS API Gateway: Creating serverless APIs

**Functionalities**:
- Data reception and storage
- Web-based interface for report generation based on stored data
- Client-specific data analysis across multiple locations

---

## 2. Data Flow

1. Raspberry Pi units collect pressure data from the attached sensors.
2. Data is transmitted in real-time to the local computer.
3. Local computer forwards the data to the AWS cloud.
4. AWS cloud stores the data in Amazon DynamoDB.
5. Users access the local computer dashboard for real-time data monitoring and the AWS-hosted web-based reporting interface for historical data analysis.

---

## 3. Network Communication

- Protocol between Raspberry Pi and Local Computer: TCP/IP
- Protocol between Local Computer and AWS Cloud: Utilizing AWS IoT Core for secure communication

---

## 4. User Interface

### 4.1 Raspberry Pi Display
- Real-time pressure display per sensor
- Average pressure within a configurable timeframe
- Time duration the pressure remains above a configurable threshold

### 4.2 Local Computer Dashboard
- Real-time data display from all connected sensor packages
- Configurable alerts for abnormal pressure readings

### 4.3 Web-based Reporting Interface
- Report generation based on historical data
- Client-specific data analysis across multiple locations

---

## 5. Data Privacy, Compliance, and Security

- Adherence to healthcare regulatory standards like HIPAA.
- AWS Key Management Service (KMS) for data encryption.
- AWS CloudTrail for logging and monitoring system activity.
- AWS IAM for fine-grained access control.

---

## 6. Monitoring, Management, and Scalability

- AWS CloudWatch for system monitoring.
- AWS CloudFormation for infrastructure management.
- Architecture allows for addition of more sensor packages and integration of enhanced analytics.

---

This proposal delineates a technical blueprint for the Remote Pressure Monitoring System, incorporating AWS services to ensure a secure, reliable, and scalable solution, critical for healthcare applications aimed at monitoring patient positioning to mitigate the risk of bed sores.