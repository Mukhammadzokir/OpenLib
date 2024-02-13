using AutoMapper;
using OpenLibrary.Domain.Entities;
using OpenLibrary.Service.DTOs.Books;
using OpenLibrary.Service.DTOs.Authors;
using OpenLibrary.Service.DTOs.Students;
using OpenLibrary.Service.DTOs.Publishers;
using OpenLibrary.Service.DTOs.BookAuthors;
using OpenLibrary.Service.DTOs.BorrowingRecords;
using OpenLibrary.Service.DTOs.Logins;
using OpenLibrary.Service.DTOs.UserCodes;
using OpenLibrary.Service.DTOs.ReturnedBooks;

namespace OpenLibrary.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book
        CreateMap<Book, BookForResultDto>().ReverseMap();
        CreateMap<Book, BookForUpdateDto>().ReverseMap();
        CreateMap<Book, BookForCreationDto>().ReverseMap();

        // Author
        CreateMap<Author, AuthorForResultDto>().ReverseMap();
        CreateMap<Author, AuthorForUpdateDto>().ReverseMap();
        CreateMap<Author, AuthorForCreationDto>().ReverseMap();

        // Student
        CreateMap<Student, StudentForResultDto>().ReverseMap();
        CreateMap<Student, StudentForUpdateDto>().ReverseMap();
        CreateMap<Student, StudentForCreationDto>().ReverseMap();

        // Publisher
        CreateMap<Publisher, PublisherForResultDto>().ReverseMap();
        CreateMap<Publisher, PublisherForUpdateDto>().ReverseMap();
        CreateMap<Publisher, PublisherForCreationDto>().ReverseMap();

        // BookAuthor
        CreateMap<BookAuthor, BookAuthorForResultDto>().ReverseMap();
        CreateMap<BookAuthor, BookAuthorForUpdateDto>().ReverseMap();
        CreateMap<BookAuthor, BookAuthorForCreationDto>().ReverseMap();

        // BorrowingRecord
        CreateMap<BorrowingRecord, BorrowingRecordForResultDto>().ReverseMap();
        CreateMap<BorrowingRecord, BorrowingRecordForUpdateDto>().ReverseMap();
        CreateMap<BorrowingRecord, BorrowingRecordForCreationDto>().ReverseMap();

        // Login
        CreateMap<LoginForCreationDto, LoginForResultDto>().ReverseMap();

        // ReturnedBook
        CreateMap<ReturnedBook, ReturnedBookForResultDto>().ReverseMap();
        CreateMap<ReturnedBook, ReturnedBookForCreationDto>().ReverseMap();

        // UserCode
        CreateMap<UserCode, UserCodeForResultDto>().ReverseMap();   
        CreateMap<UserCode, UserCodeForCreationDto>().ReverseMap();
    }
}
