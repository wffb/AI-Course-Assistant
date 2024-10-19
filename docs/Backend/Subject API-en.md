### Subject API

#### /subject/add

Add Subject

##### Request Method

**POST**

##### URL

/admin/subject/add

##### Input

| Field Name  | Type   | Description           |
| ----------- | ------ | --------------------- |
| name        | String | Subject Name          |
| identifyId  | String | Subject Identifier ID |
| description | String | Subject Description   |

##### Example Request

```
{
    "name":"SWEN90016",
    "identifyId":"90016",
    "description":"90016test"
}
```

##### Response

**Success**:

```
{
    "code": 200,
    "message": "Subject added successfully!",
    "data": null
}
```

**Failure**:

```
{
    "code": 500,
    "message": "Failed to add subject"
}
```

#### /subject/delete/{identifyId}

Delete Subject

##### Permissions

None

##### Request Method

**DELETE**

##### URL

/admin/subject/delete/{identifyId}

##### Input

Pass in the `Subject ID` as a path parameter.

##### Example Request

DELETE  /subject/delete/90016

##### Response

**Success**:

```
{
    "code": 200,
    "message": "Subject deleted successfully!"
}
```

**Failure**:

```
{
    "code": 500,
    "message": "Failed to delete subject"
}
```

#### /subject/update

Update Subject Information

##### Permissions

None

##### Request Method

**PUT**

##### URL

/admin/subject/update

##### Input

| Field Name  | Type   | Description           |
| ----------- | ------ | --------------------- |
| name        | String | Subject Name          |
| identifyId  | String | Subject Identifier ID |
| description | String | Subject Description   |

##### Example Request

```
{
    "name":"swen90016",
    "identifyId":"90016",
    "description":"90016test2"

}
```

##### Response

**Success**:

```
{
    "code": 200,
    "message": "Subject updated successfully!",
    "data":null
}
```

**Failure**:

```
{
    "code": 500,
    "message": "Failed to update subject"
}
```

#### /subject/get/{identifyId}

Retrieve by Subject ID

##### Permissions

None

##### Request Method

**GET**

##### URL

/admin/subject/get/{identifyId}

##### Input

Pass in the `Subject ID` as a path parameter.

##### Example Request

```
GET /admin/subject/get/90016
```

##### Response

**Success**:

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

**Failure**:

```
{
    "code": 500,
    "message": "Subject not found."
}
```