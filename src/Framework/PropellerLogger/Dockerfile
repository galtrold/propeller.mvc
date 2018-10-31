FROM node
COPY /dist / 
RUN /bin/bash -c "source /app/container_setup_script.sh"
ENV doMerge "false"
ENTRYPOINT ["/app/container_startup_script.sh"]
#CMD [ "/usr/local/bin/start.sh" ]
