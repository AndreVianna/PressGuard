FROM balenalib/raspberrypi4-64-debian:latest

RUN apt update sudo -y

RUN apt updgrade sudo -y

RUN apt install openssh-server sudo -y

RUN groupadd devops && useradd -ms /bin/bash -g devops devopsuser

RUN mkdir -p /home/devopsuser/.ssh

COPY idkey.pub /home/devopsuser/.ssh/authorized_keys

RUN chown devopsuser:devops /home/devopsuser/.ssh/authorized_keys && chmod 600 /home/devopsuser/.ssh/authorized_keys

RUN service ssh start
EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]