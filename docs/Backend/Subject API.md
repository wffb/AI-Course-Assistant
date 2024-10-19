### Subject API

#### /subject/add

添加科目

##### 请求方式

**POST**

##### 地址

/admin/subject/add

##### 输入

| 字段名      | 类型   | 描述       |
| ----------- | ------ | ---------- |
| name        | String | 科目名称   |
| identifyId  | String | 科目标识ID |
| description | String | 科目描述   |

##### 示例请求

```
{
    "name":"SWEN90016",
    "identifyId":"90016",
    "description":"90016test"
}
```

##### 返回

**成功**：

```
{
    "code": 200,
    "message": "Subject added successfully!",
    "data": null
}
```

**失败**：

```
json复制代码{
    "code": 500,
    "message": "Failed to add subject"
}
```

#### /subject/delete/{identifyId}

删除科目

##### 权限

无

##### 请求方式

**DELETE**

##### 地址

/admin/subject/delete/{identifyId}

##### 输入

通过路径参数传入要删除的 `Subject ID`

##### 示例请求

DELETE  /subject/delete/90016

##### 返回

**成功**：

```
{
    "code": 200,
    "message": "Subject deleted successfully!"
}
```

**失败**：

```
{
    "code": 500,
    "message": "Failed to delete subject"
}
```

#### /subject/update

更新科目信息

##### 权限

无

##### 请求方式

**PUT**

##### 地址

/admin/subject/update

##### 输入

| 字段名      | 类型   | 描述       |
| ----------- | ------ | ---------- |
| name        | String | 科目名称   |
| identifyId  | String | 科目标识ID |
| description | String | 科目描述   |

##### 示例请求

```
{
    "name":"swen90016",
    "identifyId":"90016",
    "description":"90016test2"

}
```

##### 返回

**成功**：

```
{
    "code": 200,
    "message": "Subject updated successfully!",
    "data":null
}
```

**失败**：

```
{
    "code": 500,
    "message": "Failed to update subject"
}
```

#### /subject/get/{identifyId}

按subjectid查询

##### 权限

无

##### 请求方式

**GET**

##### 地址

/admin/subject/get/{identifyId}

##### 输入

通过路径参数传入 `Subject ID`

##### 示例请求

```
GET /admin/subject/get/90016
```

##### 返回

**成功**：

```
{
    "code": 200,
    "message": "Subject retrieved successfully!",
    "data": {
        "id": "a85986996927ab27d98c56bde5d2bcbf",
        "name": "swen90016",
        "identifyId": "90016",
        "description": "90016test2"
    }
}
```

**失败**：

```
json复制代码{
    "code": 500,
    "message": "Subject not found."
}
```