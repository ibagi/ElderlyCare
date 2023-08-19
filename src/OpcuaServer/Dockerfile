FROM python:3.10.0-alpine

WORKDIR /usr/src/app

COPY ./OpcuaServer/requirements.txt ./
COPY ./OpcuaServer/server.py ./

RUN pip install --no-cache-dir -r requirements.txt
CMD [ "python", "./server.py" ]