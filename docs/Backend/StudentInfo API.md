### StudentInfo API

- port: 8080

##### Authorization
JWT Token is required for authentication.

##### Request Method

**POST**

##### Endpoint

```
/admin/addStuToSub
```

##### Input

| Field Name        | Type   | Description                             |
| ----------------- | ------ | --------------------------------------- |
| studentId         | String | The user ID associated with the student |
| subjectIdentifyId | String | The student's list of selected subjects |

##### Sample Request

```json
{
    "studentId":"5ba016a59c4c821392f00dc0a1901b81",
    "subjectIdentifyId":"90015"
}
```

##### Response

**Success**:

```
{
    "code": 200,
    "message": "Insert subject Success!",
    "data": null
}
```

**Failure**:

```
{
    "code": 500,
    "message": "Subject has already been added",
    "data": null
}

{
    "code": 500,
    "message": "user does not exist",
    "data": null
}
```